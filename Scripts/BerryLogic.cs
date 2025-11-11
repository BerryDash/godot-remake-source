using Godot;
using System;

public partial class BerryLogic : Node2D {
    string BerryType = "";

    void PlaySound(string path, float volume) {
        var player = new AudioStreamPlayer();
        player.Stream = (AudioStream)GD.Load(path);
        player.VolumeDb = Globals.SoundEffectVolume * (volume / 1000);
        GetNode<Node2D>("/root/Game").AddChild(player);
        player.Play();

        player.Finished += () => player.QueueFree(); // clean up after playing
    }

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
        } else if (Sprite == GD.Load(BerryTexturesFolder + "SpeedyBerry.png")) {
            BerryType = "speedy";
        }

        int ScoreAdder = (BerryType == "normal") ? 1 :
            (BerryType == "poison" || BerryType == "slow") ? 0 :
            (BerryType == "speedy") ? 10 : 0;

        Globals.Score += ScoreAdder;
        if (Globals.Score > Globals.HighScore) {
            Globals.HighScore += ScoreAdder;
        }

        if (BerryType == "normal") {
            PlaySound("res://Audio/SFX/Eat.mp3", 80f);
        } else if (BerryType == "poison") {
            Globals.EffectTimer = 0f;
            Globals.Score = 0;

            BazookaManager.Write(BazookaManager.HighScore, Globals.HighScore.ToString());

            PlaySound("res://Audio/SFX/Death.mp3", 20f);
        } else if (BerryType == "slow") {
            if (Globals.ActiveEffect == "slow") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "slow";

            PlaySound("res://Audio/SFX/Downgrade.mp3", 5f);
        } else if (BerryType == "ultra") {
            if (Globals.ActiveEffect == "ultra") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "ultra";

            PlaySound("res://Audio/SFX/Powerup.mp3", 5f);
        } else if (BerryType == "speedy") {
            if (Globals.ActiveEffect == "speedy") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "speedy";

            PlaySound("res://Audio/SFX/SpeedyPowerup.mp3", 5f);
        }

        QueueFree();
    }
}
