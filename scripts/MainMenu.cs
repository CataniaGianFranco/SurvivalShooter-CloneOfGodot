using Godot;

public partial class MainMenu : Control
{
	private void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main_game.tscn");
	}

	private void OnOptionsPressed()
	{
		GD.Print("Options");
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
