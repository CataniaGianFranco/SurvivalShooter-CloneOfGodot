extends Control

@onready var play_button: Button = $MarginContainer/VBoxContainer/Play
@onready var options_button: Button = $MarginContainer/VBoxContainer/Options
@onready var quitButton: Button = $MarginContainer/VBoxContainer/Quit

func _process(delta: float) -> void:
	_updateUI()

func _updateUI() -> void:
	play_button.text = "PLAY"
	options_button.text = "OPTIONS"
	quitButton.text = "QUIT"

func _on_play_pressed() -> void:
	get_tree().change_scene_to_file("res://scenes/main_game.tscn")

func _on_options_pressed() -> void:
	print("Options")

func _on_quit_pressed() -> void:
	get_tree().quit()

func _on_english_pressed() -> void:
	TranslationServer.set_locale("en")

func _on_spanish_pressed() -> void:
	TranslationServer.set_locale("sp")

func _on_japanese_pressed() -> void:
	TranslationServer.set_locale("jp")
