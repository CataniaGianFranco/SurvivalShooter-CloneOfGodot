using Godot;
using Godot.Collections;

public partial class Player : CharacterBody3D
{   
	public int Health { get { return _health; } }
	[Signal] public delegate void HealthChangedEventHandler();
	
	[Export] private AnimationTree _animationTree = null;
	[Export] private Camera3D _cameraMain = null;
	[Export] private Gun _gun = null;
	[Export] private Node3D _playerCtrl = null;
	[Export] private MeshInstance3D _playerMesh = null;

	private Vector3 _rayOrigin = Vector3.Zero;
	private Vector3 _rayEnd = Vector3.Zero;

	private int _floorMask = 0;

	private int _health = 100;
	private bool _isDeath = false;
	
	private float _cameraRayLenght = 100.0f;
	private float _speed = 6.0f;
	private float _range = 100.0f;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ui_select"))
		{
			if (_health <= 0) return;
			else
				TakeDamage(30);
		}
		
		if (!_isDeath)
		{
			MapController();
			Mouse();
			_gun.DisableEffects();
		}
	}
	
	public void TakeDamage(int amount)
	{
		_health -= amount;

		if (_health <= 0)
			SetDie();
		
		EmitSignal(SignalName.HealthChanged);
	}

	private void MapController()
	{
		float horizontal = Input.GetAxis("ui_right", "ui_left");
		float vertical = Input.GetAxis("ui_down", "ui_up");
		bool isShooting = Input.IsActionPressed("Fire1");
		
		Vector3 velocity = Velocity;
		
		Vector3 direction = (Transform.Basis * new Vector3(-horizontal, 0.0f, -vertical)).Normalized();

		SetAnimating(horizontal, vertical);

		if (direction == Vector3.Zero)
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0.0f, _speed);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0.0f, _speed);
		}
		else
			velocity = new Vector3(direction.X, 0.0f, direction.Z) * _speed;

		Velocity = velocity;
		
		MoveAndSlide();

		if (isShooting)
			_gun.Shoot();
	}

	private void SetAnimating(float horizontal, float vertical)
	{
		bool idle = (horizontal == 0.0f) && (vertical == 0.0f);
		_animationTree.Set("parameters/conditions/idle", idle);
		_animationTree.Set("parameters/conditions/is_walking", !idle);
	}

	private void Mouse()
	{
		Vector2 _mousePosition = GetViewport().GetMousePosition();

		_rayOrigin = _cameraMain.ProjectRayOrigin(_mousePosition);

		_rayEnd = _rayOrigin + _cameraMain.ProjectRayNormal(_mousePosition) * _range;

		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(_rayOrigin, _rayEnd);
		query.CollisionMask = (1 << 1);
		
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		
		Dictionary intersection = spaceState.IntersectRay(query);

		if (intersection != null)
		{
			Vector3 intersectionPoint = (Vector3)intersection["position"];
			Vector3 directionToIntersection = (intersectionPoint - GlobalTransform.Origin).Normalized();

			directionToIntersection.Y = 0.0f;
			directionToIntersection = directionToIntersection.Normalized();

			this.LookAt(GlobalTransform.Origin + (directionToIntersection), Vector3.Up);
		}
	}

	private void SetDie()
	{
		_isDeath = true;
		_animationTree.Set("parameters/conditions/is_death", true);
	}
}
