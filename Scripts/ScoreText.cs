using Godot;
using System;

public partial class ScoreText : Control {
    public override void _Process(double delta) {
        Label ScoreText = GetNode<Label>("ScoreLabel");
        Label HighScoreText = GetNode<Label>("HighScoreLabel");
        Label EffectTimerText = GetNode<Label>("EffectTimerLabel");

        ScoreText.Text = "Score: " + Globals.Score;
        HighScoreText.Text = "High Score: " + Globals.HighScore;

        if (Globals.ActiveEffect == "none") {
            EffectTimerText.Text = "";
        } else {
            string EffectTimerType = "";
            if (Globals.ActiveEffect == "ultra") {
                EffectTimerType = "Boost";
            } else if (Globals.ActiveEffect == "slow") {
                EffectTimerType = "Slowness";
            } else if (Globals.ActiveEffect == "speedy") {
                EffectTimerType = "Speed";
            }
            EffectTimerText.Text = EffectTimerType + " " + "expires in: " + Math.Floor(Globals.EffectTimer * 10) / 10 + "s";
        }
    }
}
