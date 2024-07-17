using Godot;

public partial class ZombieBunny : CharacterBody3D
{
    [Export] private CharacterBody3D _player = null;

    private float _health = 25.0f;

    private void OnArea3dBodyEntered(Node3D body)
    {
        GD.Print("Zombunny is attacking you.");
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0.0f)
            QueueFree();
    }
}