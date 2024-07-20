using Godot;

public partial class GameManager : Node
{
    public override void _Ready()
    {
        SetCursorMode();
    }

    private void SetCursorMode()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }
}
