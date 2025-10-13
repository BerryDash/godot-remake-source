using Godot;
using System;

public partial class PauseMenuContinueButton : Button {
    public void _on_button_up() {
        Globals.IsPaused = false;
        GetNode<Control>("../..").QueueFree();
    }
}