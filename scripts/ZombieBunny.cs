using Godot;

public partial class ZombieBunny : CharacterBody3D
{
    [Export] private CharacterBody3D _player = null;


    private Vector3 _velocity = Vector3.Zero;
    private float _health = 25.0f;
    private float _speed = 100.0f;
    private float _rotationSpeed = 3.0f;

    public override void _Process(double delta)
    {
        FollowTarget(delta);

        MoveAndSlide();
    }

    private void OnArea3dBodyEntered(Node3D body)
    {
        //GD.Print("Zombunny is attacking you.");
    }

    private void FollowTarget(double delta)
    {
        if (_player == null) return;

        Vector3 targetDirection = (_player.GlobalTransform.Origin - this.GlobalTransform.Origin).Normalized();

        Velocity = new Vector3(targetDirection.X, 0.0f, targetDirection.Z) * _speed * (float)delta;

        Quaternion targetRotation = new Quaternion(Vector3.Up, Mathf.Atan2(targetDirection.X, targetDirection.Z));
        Quaternion adjustment = new Quaternion(Vector3.Up, Mathf.Pi);
        targetRotation = targetRotation * adjustment;
        Quaternion currentRotation = GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion newRotation = currentRotation.Slerp(targetRotation, _rotationSpeed * (float)delta);
        //this.GlobalTransform = new Transform3D(newRotation, GlobalTransform.Origin);
        Transform3D newTransform = GlobalTransform;
        newTransform.Basis = new Basis(newRotation);
        GlobalTransform = newTransform;

        //LookAt(_player.GlobalTransform.Origin, Vector3.Up);
        
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        if (_health <= 0.0f)
            QueueFree();
    }
}