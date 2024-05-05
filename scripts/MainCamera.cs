using Godot;

public partial class MainCamera : Camera3D
{
	[Export] private CharacterBody3D _target;

	private Vector3 _offset = Vector3.Zero;

	private float _smoothing = 5.0f;

    public override void _Ready()
    {
		InitializeOffset();
    }

    public override void _PhysicsProcess(double delta)
    {
        SmoothFollowTarget(delta);
    }

	private void InitializeOffset()
	{
		_offset = GlobalPosition - _target.Position;
	}

	private void SmoothFollowTarget(double delta)
	{
		Vector3 targetCameraPosition = _target.Position + _offset;

		float x = Mathf.Lerp(GlobalPosition.X, targetCameraPosition.X, _smoothing * (float)delta);
    	float y = Mathf.Lerp(GlobalPosition.Y, targetCameraPosition.Y, _smoothing * (float)delta);
    	float z = Mathf.Lerp(GlobalPosition.Z, targetCameraPosition.Z, _smoothing * (float)delta);

		GlobalPosition = new Vector3(x, y, z);
	}
}
