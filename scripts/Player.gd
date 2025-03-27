class_name Player extends CharacterBody3D

signal health_changed

var Health: float:
	get: return _health
	set(value):
		if _health != value:
			_health = value
			emit_signal("health_changed", _health)

@export var camera_main: Camera3D = null

@onready var animation_tree: AnimationTree = $AnimationTree
@onready var gun: Gun = $PlayerCtrl/Ctrl_Grp
@onready var player_ctrl: Node3D = $PlayerCtrl
@onready var player_mesh: MeshInstance3D = $PlayerCtrl/Skeleton3D/Player
@onready var gun_barrel_end: Marker3D = $PlayerCtrl/Ctrl_Grp/GunBarrelEnd

var _ray_origin: Vector3 = Vector3.ZERO
var _ray_end: Vector3 = Vector3.ZERO
var _floor_mask: int = 0
var _health: float = 100
var _movement_speed: float = 6.0
var _range: float = 100.0
var _is_death: bool = false
var _camera_ray_length: float = 100.0

func _process(delta: float) -> void:
	if Input.is_action_just_released("ui_select"):
		if _health <= 0:
			return
		else:
			take_damage(30)

	if not _is_death:
		_map_controller()
		_mouse(delta)
		#_look_aim_with_marker()
		gun.disable_effects()

func take_damage(amount: int) -> void:
	_health -= amount
	if _health <= 0:
		set_die()
	emit_signal("health_changed")

func _map_controller() -> void:
	var horizontal: float = Input.get_axis("ui_right", "ui_left")
	var vertical: float = Input.get_axis("ui_down", "ui_up")
	var is_shooting: bool = Input.is_action_pressed("Fire1")
	
	var direction: Vector3 = (transform.basis * Vector3(-horizontal, 0.0, -vertical)).normalized()

	_set_animating(horizontal, vertical)

	if direction == Vector3.ZERO:
		velocity.x = move_toward(velocity.x, 0.0, _movement_speed)
		velocity.z = move_toward(velocity.z, 0.0, _movement_speed)
	else:
		velocity = Vector3(direction.x, 0.0, direction.z) * _movement_speed

	velocity = velocity
	move_and_slide()

	if is_shooting:
		gun.shoot()

func _set_animating(horizontal: float, vertical: float) -> void:
	var idle: bool = horizontal == 0.0 and vertical == 0.0
	animation_tree.set("parameters/conditions/idle", idle)
	animation_tree.set("parameters/conditions/is_walking", not idle)

func _look_aim_with_marker() -> void:
	var direction_to_marker: Vector3 = (gun_barrel_end.global_transform.origin - global_transform.origin).normalized()
	direction_to_marker.y = 0.0
	direction_to_marker = direction_to_marker.normalized()
	
	var target_angle = atan2(direction_to_marker.x, direction_to_marker.z)
	var current_angle = rotation.y
	rotation.y = lerp_angle(current_angle, target_angle, 0.1)  # Ajusta la velocidad de rotación

func _mouse(delta: float) -> void:
	var mouse_position: Vector2 = get_viewport().get_mouse_position()
	_ray_origin = camera_main.project_ray_origin(mouse_position)
	_ray_end = _ray_origin + camera_main.project_ray_normal(mouse_position) * _range

	var query: PhysicsRayQueryParameters3D = PhysicsRayQueryParameters3D.new()
	query.from = _ray_origin
	query.to = _ray_end
	query.collision_mask = 1 << 1

	var space_state: PhysicsDirectSpaceState3D = get_world_3d().direct_space_state
	var intersection: Dictionary = space_state.intersect_ray(query)

	if intersection.has("position"):
		var intersection_point: Vector3 = intersection["position"]
		var direction_to_intersection: Vector3 = (intersection_point - global_transform.origin).normalized()

		direction_to_intersection.y = 0.0
		direction_to_intersection = direction_to_intersection.normalized()

		look_at(global_transform.origin + direction_to_intersection, Vector3.UP)

func set_die() -> void:
	_is_death = true
	animation_tree.set("parameters/conditions/is_death", true)
