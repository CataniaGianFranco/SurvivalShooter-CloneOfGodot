class_name MainCamera extends Camera3D

@export var target: CharacterBody3D = null
@export var smoothing: float = 5.0

var _offset: Vector3 = Vector3.ZERO

func _ready() -> void:
	if target:
		_initialize_offset()

func _physics_process(delta: float) -> void:
	if target:
		_smooth_follow_target(delta)

func _initialize_offset() -> void:
	_offset = global_transform.origin - target.global_transform.origin

func _smooth_follow_target(delta: float) -> void:
	var target_camera_position: Vector3 = target.global_transform.origin + _offset
	global_transform.origin = global_transform.origin.lerp(target_camera_position, smoothing * delta)
