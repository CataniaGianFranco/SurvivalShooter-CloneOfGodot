class_name ZombieBunny extends CharacterBody3D

@export var player : CharacterBody3D

var health: float = 25.0
var speed: float = 100.0
var rotation_speed: float = 3.0

func _process(delta: float) -> void:
	follow_target(delta)
	move_and_slide()

func _on_area_3d_body_entered(body : Node3D) -> void:
	# print("Zombunny is attacking you.")
	pass

# Method to follow the target (player)
func follow_target(delta: float) -> void:
	if player == null:
		return

	var target_direction : Vector3 = (player.global_transform.origin - global_transform.origin).normalized()

	velocity = Vector3(target_direction.x, 0.0, target_direction.z) * speed * delta

	var target_rotation: Quaternion = Quaternion(Vector3.UP,rad_to_deg(atan2(target_direction.x, target_direction.z)))
	var adjustment: Quaternion = Quaternion(Vector3.UP, rad_to_deg(180))
	target_rotation *= adjustment

	var current_rotation : Quaternion = global_transform.basis.get_rotation_quaternion()
	var new_rotation : Quaternion = current_rotation.slerp(target_rotation, rotation_speed * delta)

	var new_transform : Transform3D = global_transform
	new_transform.basis = Basis(new_rotation)
	global_transform = new_transform

# Method to handle damage taken
func take_damage(damage: float) -> void:
	health -= damage
	if health <= 0.0:
		queue_free()
