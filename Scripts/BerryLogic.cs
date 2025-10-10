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
        } else if (Sprite == GD.Load(BerryTexturesFolder + "PoisonBerry.png")) {
            BerryType = "poison";
        } else if (Sprite == GD.Load(BerryTexturesFolder + "SlowBerry.png")) {
            BerryType = "slow";
        }

        int ScoreAdder = (BerryType == "normal") ? 1 :
            (BerryType == "poison" || BerryType == "slow") ? 0 : 5;

        Globals.Score += ScoreAdder;
        if (Globals.Score > Globals.HighScore) {
            Globals.HighScore += ScoreAdder;
        }

        if (BerryType == "poison") {
            Globals.EffectTimer = 0f;
            Globals.Score = 0;
        } else if (BerryType == "ultra") {
            if (Globals.ActiveEffect == "ultra") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "ultra";
        }

        QueueFree();
    }
}
