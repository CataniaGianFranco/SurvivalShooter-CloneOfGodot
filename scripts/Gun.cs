using Godot;

public partial class Gun : Node3D
{
	[Export] private Marker3D _gunBarrelEnd = null;
	[Export] private AudioStreamPlayer _gunAudio = null;
	[Export] private Light3D _gunLight = null;
	[Export] private RayCast3D _gunRay = null;

	private float _scaled_delta = 0.0f;
	private float _timer = 0.0f;

	private const float _TIME_SCALE = 1.0f;
	private const float _TIME_BETWEEN_BULLETS = 0.15F;
	private const float _EFFECTS_DISPLAY_TIME = 0.20f;

    public override void _Process(double delta)
    {
        _timer += (float)delta;
		_scaled_delta = (float)delta * _TIME_SCALE;
    }

    public void Shoot()
	{		
		if (_scaled_delta != 0.0f && _timer >= _TIME_BETWEEN_BULLETS)
		{
			_timer = 0.0f;
			_gunAudio.Play();
			_gunLight.Visible = true;
		}
		DisableEffects();
	}

	private void DisableEffects()
	{

		if (_timer >= (_TIME_BETWEEN_BULLETS * _EFFECTS_DISPLAY_TIME))
		{
			_gunLight.Visible = false;
		}

	}

}