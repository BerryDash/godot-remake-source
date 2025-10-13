using Godot;
using System;

public partial class BackToMenuLogic : Control {
    public void _on_menu_button_up() {
        Globals.IsPaused = false;
        Globals.Score = 0;
        Globals.ActiveEffect = "none";
        Globals.EffectTimer = 0f;

        BazookaManager.Write(BazookaManager.HighScore, Globals.HighScore.ToString());
    }
}
