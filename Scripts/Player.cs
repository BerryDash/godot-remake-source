using Godot;
using System;

public partial class Player : Node2D {
    private float MovementSpeed = 800f;
    private float ScreenBorders = 120f;

    public override void _Ready() {
        Position = new Vector2(x: DisplayServer.WindowGetSize().X / 2, y: 0);
    }

    public override void _Process(double delta) {
        var ScreenSize = GetViewport().GetVisibleRect().Size;
        var WindowSize = DisplayServer.WindowGetSize();
        var WindowScale = WindowSize / ScreenSize;

        Vector2 Pos = Position;
        Pos.Y = (WindowSize.Y / WindowScale.Y) - 100;

        if ((Input.IsKeyPressed((Key)Key.Left) || Input.IsKeyPressed((Key)Key.A)) && Pos.X > ScreenBorders) {
            GetNode<Sprite2D>("Texture").FlipH = false;
            Pos.X -= MovementSpeed * (float)delta;
        }

        float ScreenWidth = (WindowSize.X / WindowScale.X) - ScreenBorders;
        if ((Input.IsKeyPressed((Key)Key.Right) || Input.IsKeyPressed((Key)Key.D)) && Pos.X < ScreenWidth) {
            GetNode<Sprite2D>("Texture").FlipH = true;
            Pos.X += MovementSpeed * (float)delta;
        }

        Position = Pos;
    }
}
