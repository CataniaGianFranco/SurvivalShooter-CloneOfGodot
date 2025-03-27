class_name Gun extends Node3D

const TIME_SCALE: float = 1.0
const TIME_BETWEEN_BULLETS: float = 0.15
const EFFECTS_DISPLAY_TIME: float = 0.20

@onready var gun_audio: AudioStreamPlayer = $"../../AudioShoot"
@onready var gun_barrel_end: Marker3D = $GunBarrelEnd
@onready var gun_light: OmniLight3D = $GunBarrelEnd/OmniLight3D
@onready var laser: RayCast3D = $GunBarrelEnd/Laser

var _scaled_delta: float = 0.0
var _timer: float = 0.0
var _damage: float = 10.0

func _process(delta: float) -> void:
	_timer += delta
	_scaled_delta = delta * TIME_SCALE

func shoot() -> void:
	if _scaled_delta != 0.0 and _timer >= TIME_BETWEEN_BULLETS:
		detect_collider()
		_timer = 0.0
		gun_audio.play()
		laser.visible = true
		gun_light.visible = true;

func disable_effects() -> void:
	if _timer >= (TIME_BETWEEN_BULLETS * EFFECTS_DISPLAY_TIME):
		laser.visible = false
		gun_light.visible = false

func detect_collider() -> void:
	if laser.is_colliding():
		var collider: CharacterBody3D = laser.get_collider() as CharacterBody3D
		if collider != null and collider is ZombieBunny:
			collider.take_damage(_damage)
