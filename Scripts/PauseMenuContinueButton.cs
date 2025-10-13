using Godot;
using System;

public partial class PauseMenuContinueButton : Button {
    public void _on_button_up() {
        AudioStreamPlayer MusicPlayer = GetNode<AudioStreamPlayer>("/root/Game/MusicPlayer");

        MusicPlayer.StreamPaused = !MusicPlayer.StreamPaused;

        Globals.IsPaused = false;
        GetNode<Control>("../..").QueueFree();
    }
}