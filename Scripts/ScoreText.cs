using Godot;
using System;

public partial class ScoreText : Control {
    public override void _Process(double delta) {
        Label ScoreText = GetNode<Label>("ScoreLabel");
        Label HighScoreText = GetNode<Label>("HighScoreLabel");

        ScoreText.Text = "Score: " + Globals.Score;
        HighScoreText.Text = "High Score: " + Globals.HighScore;
    }
}
