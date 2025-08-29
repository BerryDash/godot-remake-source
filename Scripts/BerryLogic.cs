using Godot;
using System;

public partial class BerryLogic : Node2D {
    private void _on_hitbox_area_entered(Area2D area) {
        Globals.Score += 1;
        if (Globals.Score > Globals.HighScore) {
            Globals.HighScore += 1;
        }

        QueueFree();
    }
}
