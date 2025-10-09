using Godot;
using System;

public partial class BerryLogic : Node2D {
    string BerryType = "";

    private void _on_hitbox_area_entered(Area2D area) {
        Texture2D Sprite = GetNode<Sprite2D>("Sprite").Texture;

        string BerryTexturesFolder = "res://Textures/Berries/";
        if (Sprite == GD.Load(BerryTexturesFolder + "Berry.png")) {
            BerryType = "normal";
        } else if (Sprite == GD.Load(BerryTexturesFolder + "UltraBerry.png")) {
            BerryType = "ultra";
        }

        int ScoreAdder = (BerryType == "normal") ? 1 : 5;
        Globals.Score += ScoreAdder;
        if (Globals.Score > Globals.HighScore) {
            Globals.HighScore += ScoreAdder;
        }

        QueueFree();
    }
}
