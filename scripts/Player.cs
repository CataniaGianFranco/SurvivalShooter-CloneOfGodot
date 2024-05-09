using Godot;
using Godot.Collections;

public partial class Player : CharacterBody3D
{    
    [Export] private AnimationTree _animationTree = null;
    [Export] private Camera3D _cameraMain = null;
    [Export] private Gun _gun = null;
    [Export] private Node3D _playerCtrl = null;
    [Export] private MeshInstance3D _playerMesh = null;
    [Export] private ShaderMaterial _dissolverShader = null;
    
    private Vector3 _rayOrigin = Vector3.Zero;
    private Vector3 _rayEnd = Vector3.Zero;

    private int _floorMask = 0;

    private float _cameraRayLenght = 100.0f;
    private float _speed = 6.0f;
    private float _range = 100.0f;

    public override void _Ready()
    {
        Tween tween = this.CreateTween();
        tween.SetEase(Tween.EaseType.Out)
       
    }

    public override void _PhysicsProcess(double delta)
    {
        MapController();
    }

    /*public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            switch (mouseEvent.ButtonIndex)
            {
                case MouseButton.Left:
                _gun.Shoot();
                break;
            }
        }
    }*/

    private void MapController()
    {
        float horizontal = Input.GetAxis("ui_right", "ui_left");
        float vertical = Input.GetAxis("ui_down", "ui_up");
        
        Vector3 velocity = Velocity;
        
        Vector3 direction = (Transform.Basis * new Vector3(horizontal, 0.0f, vertical)).Normalized();

        Animating(horizontal, vertical);

        if (direction == Vector3.Zero)
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0.0f, _speed);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0.0f, _speed);
        }
        else
            velocity = new Vector3(direction.X, 0.0f, direction.Z) * _speed;

        Velocity = velocity;
        MoveAndSlide();

        Mouse();

        if (Input.IsActionPressed("Fire1"))
            _gun.Shoot();
    }

    private void Animating(float horizontal, float vertical)
    {
        bool idle = (horizontal == 0.0f) && (vertical == 0.0f);
        _animationTree.Set("parameters/conditions/idle", idle);
        _animationTree.Set("parameters/conditions/is_walking", !idle);
    }

    private void Mouse()
    {
        PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
        
        Vector2 _mousePosition = GetViewport().GetMousePosition();

        _rayOrigin = _cameraMain.ProjectRayOrigin(_mousePosition);
        _rayEnd = _rayOrigin + _cameraMain.ProjectRayNormal(_mousePosition) * _range;

        PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(_rayOrigin, _rayEnd);
        query.CollisionMask = (1 << 1);
        
        Dictionary intersection = spaceState.IntersectRay(query);

        if (intersection != null)
        {
            Vector3 intersectionPoint = (Vector3)intersection["position"];
            Vector3 directionToIntersection = (intersectionPoint - GlobalTransform.Origin).Normalized();

            directionToIntersection.Y = 0.0f;
            directionToIntersection = directionToIntersection.Normalized();

            this.LookAt(GlobalTransform.Origin + (-directionToIntersection), Vector3.Up);
        }
    }
}
