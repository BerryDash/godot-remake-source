using Godot;
using System;

public partial class Berries : Node2D {
    private float SpawnBerryTimer = 0f;

    public override void _Process(double delta) {
        Vector2 Pos = Position;

        if (!Globals.IsPaused) {
            Pos.Y += Globals.BerrySpeed * (float)delta;

            SpawnBerryTimer += (float)delta;
        }

        int ScreenWidth = DisplayServer.WindowGetSize().X;

        if (SpawnBerryTimer > 1.0) {
            SpawnBerryTimer = 0f;

            PackedScene Berry = GD.Load<PackedScene>("res://Prefabs/Berries/Berry.tscn");

            Node2D BerryInstance = Berry.Instantiate<Node2D>();

            BerryInstance.Position = new Vector2(x: new Random().Next(-(ScreenWidth / 2), ScreenWidth / 2), y: -100 - Position.Y);

            Sprite2D BerrySprite = BerryInstance.GetNode<Sprite2D>("Sprite");

            int BerryChance = new Random().Next(1, 101);

            if (BerryChance <= 60) {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/Berry.png");
            } else if (BerryChance <= 75) {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/PoisonBerry.png");
            } else if (BerryChance <= 85) {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/SlowBerry.png");
            } else if (BerryChance <= 95) {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/UltraBerry.png");
            } else {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/SpeedyBerry.png");
            }

            AddChild(BerryInstance);
        }

        Position = Pos;
    }
}
