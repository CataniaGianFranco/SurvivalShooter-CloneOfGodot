class_name Laser extends RayCast3D

@onready var beam_mesh : MeshInstance3D = $BeamMesh
@onready var end_particles: GPUParticles3D = $EndParticles

func _process(delta: float) -> void:
	laser_hit()

func laser_hit() -> void:
	var cast_point : Vector3 = Vector3.ZERO

	force_raycast_update()

	if is_colliding():
		cast_point = to_local(get_collision_point())

		if beam_mesh.mesh is CylinderMesh:
			beam_mesh.mesh.height = cast_point.y

		beam_mesh.position = Vector3(0.0, cast_point.y / 2, 0.0)
		end_particles.position = Vector3(0.0, cast_point.y, 0.0)
