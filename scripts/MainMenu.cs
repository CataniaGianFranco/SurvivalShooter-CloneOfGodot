using Godot;

public partial class MainMenu : Control
{
	[Export] private Button _playButton = null;
	[Export] private Button _optionsButton = null;
	[Export] private Button _quitButton = null;

    public override void _Process(double delta)
    {
        UpdateUI();
    }

	private void UpdateUI()
	{
		_playButton.Text = "PLAY";
		_optionsButton.Text = "OPTIONS";
		_quitButton.Text = "QUIT";
	}

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

	private void OnEnglishPressed() => TranslationServer.SetLocale("en");

	private void OnSpanishPressed() => TranslationServer.SetLocale("sp");

	private void OnJapanesePressed() => TranslationServer.SetLocale("jp");
}
