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

        AudioStreamPlayer BerrySoundPlayer = GetNode<AudioStreamPlayer>("/root/Game/BerrySoundPlayer");
        if (BerryType == "normal") {
            BerrySoundPlayer.Stream = (AudioStream)GD.Load("res://Audio/SFX/Eat.mp3");
            BerrySoundPlayer.VolumeDb = Globals.SoundEffectVolume * 0.08f;
            BerrySoundPlayer.Play();
        } else if (BerryType == "poison") {
            Globals.EffectTimer = 0f;
            Globals.Score = 0;

            BazookaManager.Write(BazookaManager.HighScore, Globals.HighScore.ToString());

            BerrySoundPlayer.Stream = (AudioStream)GD.Load("res://Audio/SFX/Death.mp3");
            BerrySoundPlayer.VolumeDb = Globals.SoundEffectVolume * 0.05f;
            BerrySoundPlayer.Play();
        } else if (BerryType == "slow") {
            if (Globals.ActiveEffect == "slow") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "slow";

            BerrySoundPlayer.Stream = (AudioStream)GD.Load("res://Audio/SFX/Downgrade.mp3");
            BerrySoundPlayer.VolumeDb = Globals.SoundEffectVolume * 0.005f;
            BerrySoundPlayer.Play();
        } else if (BerryType == "ultra") {
            if (Globals.ActiveEffect == "ultra") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "ultra";

            BerrySoundPlayer.Stream = (AudioStream)GD.Load("res://Audio/SFX/Powerup.mp3");
            BerrySoundPlayer.VolumeDb = Globals.SoundEffectVolume * 0.005f;
            BerrySoundPlayer.Play();
        } else if (BerryType == "speedy") {
            if (Globals.ActiveEffect == "speedy") {
                Globals.EffectTimer += 10f;
            } else {
                Globals.EffectTimer = 10f;
            }

            Globals.ActiveEffect = "speedy";

            BerrySoundPlayer.Stream = (AudioStream)GD.Load("res://Audio/SFX/SpeedyPowerup.mp3");
            BerrySoundPlayer.VolumeDb = Globals.SoundEffectVolume * 0.005f;
            BerrySoundPlayer.Play();
        }

        QueueFree();
    }
}
