using Godot;
using System;

public partial class Player : Node2D {
    private float ScreenBorders = 120f;
    private float VelocityY = 0f;

    public override void _Ready() {
        var ScreenSize = GetViewport().GetVisibleRect().Size;
        var WindowSize = DisplayServer.WindowGetSize();
        var WindowScale = WindowSize / ScreenSize;

        Position = new Vector2(x: DisplayServer.WindowGetSize().X / 2, y: (WindowSize.Y / WindowScale.Y) - 100);
    }

    public override void _Process(double delta) {
        var ScreenSize = GetViewport().GetVisibleRect().Size;
        var WindowSize = DisplayServer.WindowGetSize();
        var WindowScale = WindowSize / ScreenSize;

        Vector2 Pos = Position;

        if (!Globals.IsPaused) {
            if ((Input.IsKeyPressed((Key)Key.Left) || Input.IsKeyPressed((Key)Key.A)) && Pos.X > ScreenBorders) {
                GetNode<Sprite2D>("Texture").FlipH = false;
                Pos.X -= Globals.MovementSpeed * (float)delta;
            }

            float ScreenWidth = (WindowSize.X / WindowScale.X) - ScreenBorders;
            if ((Input.IsKeyPressed((Key)Key.Right) || Input.IsKeyPressed((Key)Key.D)) && Pos.X < ScreenWidth) {
                GetNode<Sprite2D>("Texture").FlipH = true;
                Pos.X += Globals.MovementSpeed * (float)delta;
            }

            if (Input.IsKeyPressed((Key)Key.Space) && Pos.Y >= 976) {
                VelocityY = Globals.JumpHeight;
                GetNode<AudioStreamPlayer>("JumpSoundPlayer").Play();
            }
        }

        if (VelocityY < 600f) {
            VelocityY += Globals.Gravity * (float)delta;
        }

        Pos.Y += VelocityY * (float)delta;

        if (Pos.Y > 976) {
            Pos.Y = 976;
            VelocityY = 0;
        }

        Position = Pos;

        // Effect handling
        if (Globals.ActiveEffect != "none" && !Globals.IsPaused) {
            Globals.EffectTimer -= (float)delta;
        }

        if (Globals.EffectTimer < 0f) {
            Globals.EffectTimer = 0f;
            Globals.ActiveEffect = "none";
        }

        if (Globals.ActiveEffect == "none") {
            Globals.MovementSpeed = Globals.DefaultMovementSpeed;
        } else if (Globals.ActiveEffect == "ultra") {
            Globals.MovementSpeed = Globals.DefaultMovementSpeed * 2f;
        } else if (Globals.ActiveEffect == "slow") {
            Globals.MovementSpeed = Globals.DefaultMovementSpeed / 2f;
        }

        if (Input.IsActionJustPressed("EscapeKey") && !Globals.IsPaused) {
            PackedScene PauseMenu = GD.Load<PackedScene>("res://Scenes/PauseMenu.tscn");

            Control PauseMenuInstance = PauseMenu.Instantiate<Control>();

            PauseMenuInstance.Position = new Vector2(x: 0, y: 0);
            PauseMenuInstance.ZIndex = -1;

            GetNode<CanvasLayer>("/root/Game/CanvasLayer").AddChild(PauseMenuInstance);
            Globals.IsPaused = true;

            AudioStreamPlayer MusicPlayer = GetNode<AudioStreamPlayer>("/root/Game/MusicPlayer");

            MusicPlayer.StreamPaused = !MusicPlayer.StreamPaused;
        }
    }
}
