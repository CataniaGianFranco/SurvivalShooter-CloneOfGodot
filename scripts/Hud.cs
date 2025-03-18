using Godot;

public partial class Hud : CanvasLayer
{
	[Export] private TextureProgressBar _health = null;
	[Export] private CharacterBody3D _player = null;
    private Player _playerScript = null;


    public override void _Ready()
    {
		_playerScript = _player as Player;
        _playerScript.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _health.Value = _playerScript.Health;
    }
}
