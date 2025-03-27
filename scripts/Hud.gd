extends CanvasLayer

@onready var _health: TextureProgressBar = $HealthUI/Health
@export var _player: CharacterBody3D = null

var _playerScript: Player

func _ready() -> void:
	_playerScript = _player as Player
	_playerScript.health_changed.connect(OnHealthChanged)

func OnHealthChanged(new_health: float) -> void:
	_update_health_bar(new_health)

func _update_health_bar(new_health: float) -> void:
	_health.value = new_health
