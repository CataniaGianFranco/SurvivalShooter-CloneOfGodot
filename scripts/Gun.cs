using Godot;

public partial class Gun : Node3D
{
	[Export] private Marker3D _gunBarrelEnd = null;
	[Export] private AudioStreamPlayer _gunAudio = null;
	[Export] private OmniLight3D _gunLight = null;
	[Export] private RayCast3D _rayCast = null;

	private float _scaledDelta = 0.0f;
	private float _timer = 0.0f;
	private float _damage = 10.0f;

	private const float _TIME_SCALE = 1.0f;
	private const float _TIME_BETWEEN_BULLETS = 0.15F;
	private const float _EFFECTS_DISPLAY_TIME = 0.20f;

    public override void _Process(double delta)
    {
        _timer += (float)delta;
		_scaledDelta = (float)delta * _TIME_SCALE;

		DetectCollider();
    }

	private void DetectCollider()
	{
		if (_rayCast.IsColliding())
		{	
			CharacterBody3D collider = _rayCast.GetCollider() as CharacterBody3D;
			if (collider != null && collider is ZombieBunny zombunny)
			{
				zombunny.TakeDamage(_damage);
			}
		}
	}

    public void Shoot()
	{		
		if (_scaledDelta != 0.0f && _timer >= _TIME_BETWEEN_BULLETS)
		{
			_timer = 0.0f;
			_gunAudio.Play();
			_rayCast.Enabled = true;
			_gunLight.Visible = true;
		}
	}

	public void DisableEffects()
	{
		if (_timer >= (_TIME_BETWEEN_BULLETS * _EFFECTS_DISPLAY_TIME))
		{
			_rayCast.Enabled = false;
			_gunLight.Visible = false;
		}
	}
	
}