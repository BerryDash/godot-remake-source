using Godot;
using System;

public partial class VersionChecker : Control {
    public override void _Ready() {
        Label VersionLabel = GetNode<Label>("Version");
        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        VersionLabel.Text = "Version: " + GameVersion;
    }
}
