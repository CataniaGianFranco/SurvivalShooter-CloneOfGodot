using Godot;

public partial class Laser : RayCast3D
{
	[Export] private MeshInstance3D _beamMesh = null;


	public override void _Process(double delta)
	{
		LaserHit();
	}

	private void LaserHit()
	{
		Vector3 castPoint = Vector3.Zero;

		ForceRaycastUpdate();

		if (IsColliding())
		{
			castPoint = ToLocal(GetCollisionPoint());

			if (_beamMesh.Mesh is CylinderMesh cylinderMesh)
				cylinderMesh.Height = castPoint.Y;

			_beamMesh.Position = new Vector3(0.0f, castPoint.Y/2, 0.0f);
		}
	}
}
