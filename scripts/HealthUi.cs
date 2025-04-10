using Godot;

public partial class HealthUi : TextureProgressBar
{
	[Export] private CharacterBody3D _player = null;
    private Player _playerScript = null;


    public override void _Ready()
    {
		_playerScript = _player as Player;
        //_playerScript.Connect("HealthChangedEventHandler", this, nameof(OnHealthChanged));
        UpdateHealthBar();
    }

    private void OnHealthChanged()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        Value = _playerScript.Health;
    }
}
