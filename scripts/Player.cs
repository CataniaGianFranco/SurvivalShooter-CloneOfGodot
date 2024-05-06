using Godot;

public partial class Player : CharacterBody3D
{
    [Export] private AnimationTree _animationTree;

    private int _floorMask = 0;

    private float _speed = 6.0f;
    private float _cameraRayLenght = 100.0f;

    public override void _Ready()
    {
        /*_animationTree.Set("parameters/conditions/idle", true);
        _animationTree.Set("parameters/conditions/is_walking", false);*/
    }

    public override void _PhysicsProcess(double delta)
    {
        MapController();
    }

    private void MapController()
    {
        float _horizontal = Input.GetAxis("ui_left", "ui_right");
        float _vertical = Input.GetAxis("ui_down", "ui_up");

        GD.Print($"Horizontal: {_horizontal}");
        GD.Print($"Vertical: {_vertical}");

        Animating(_horizontal, _vertical);
    }

    private void Animating(float horizontal, float vertical)
    {
        bool idle = (horizontal == 0.0f) && (vertical == 0.0f);
        _animationTree.Set("parameters/conditions/idle", idle);
        _animationTree.Set("parameters/conditions/is_walking", !idle);
    }
}
