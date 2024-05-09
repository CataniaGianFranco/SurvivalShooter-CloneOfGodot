using Godot;

public partial class HealthUi : TextureProgressBar
{
	private int _health = 100;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ui_select"))
		{
			if (_health == 0) return;
			else
			{
				_health -= 10;
				Value = _health;
			}			
		}
	}
}
