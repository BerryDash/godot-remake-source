using Godot;
using System;

public partial class Player : Node2D {
    private float MovementSpeed = 800f;

    public override void _Ready() {
        Position = new Vector2(x: DisplayServer.WindowGetSize().X / 2, y: 0);
    }

    public override void _Process(double delta) {
        var ScreenSize = GetViewport().GetVisibleRect().Size;
        var WindowSize = DisplayServer.WindowGetSize();
        var Scale = WindowSize / ScreenSize;

        Vector2 Pos = Position;
        Pos.Y = (DisplayServer.WindowGetSize().Y - 90) / Scale.Y;

        if (Input.IsKeyPressed((Key)Key.Left) || Input.IsKeyPressed((Key)Key.A)) {
            Pos.X -= MovementSpeed * (float)delta;
        }

        if (Input.IsKeyPressed((Key)Key.Right) || Input.IsKeyPressed((Key)Key.D)) {
            Pos.X += MovementSpeed * (float)delta;
        }

        Position = Pos;
    }
}
