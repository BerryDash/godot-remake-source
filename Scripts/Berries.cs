using Godot;
using System;

public partial class Berries : Node2D {
    private float SpawnBerryTimer = 0f;

    public override void _Process(double delta) {
        Vector2 Pos = Position;
        Pos.Y += 500f * (float)delta;

        SpawnBerryTimer += (float)delta;

        int ScreenWidth = DisplayServer.WindowGetSize().X;

        if (SpawnBerryTimer > 1.0) {
            SpawnBerryTimer = 0f;

            PackedScene Berry = GD.Load<PackedScene>("res://Prefabs/Berries/Berry.tscn");

            Node2D BerryInstance = Berry.Instantiate<Node2D>();

            BerryInstance.Position = new Vector2(x: new Random().Next(-(ScreenWidth / 2), ScreenWidth / 2), y: -100 - Position.Y);

            Sprite2D BerrySprite = BerryInstance.GetNode<Sprite2D>("Sprite");

            int BerryChance = new Random().Next(1, 101);

            if (BerryChance <= 80) {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/Berry.png");
            } else {
                BerrySprite.Texture = (Texture2D)GD.Load("res://Textures/Berries/UltraBerry.png");
            }

            AddChild(BerryInstance);
        }

        Position = Pos;
    }
}
