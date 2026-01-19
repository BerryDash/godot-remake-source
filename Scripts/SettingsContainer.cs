using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class SettingsContainer : VBoxContainer
{
    private static bool ignoreToggle = true;

    public override void _Ready() {
        List<string> settings = Utils.LoadSettings();

        string fullscreen = settings.ElementAtOrDefault(0) ?? "1";
        string vsync = settings.ElementAtOrDefault(1) ?? "1";
        string showfps = settings.ElementAtOrDefault(2) ?? "0";
        GetNode<CheckBox>("Fullscreen/CheckBox").ButtonPressed = fullscreen == "1";
        GetNode<CheckBox>("VSync/CheckBox").ButtonPressed = vsync == "1";
        GetNode<CheckBox>("ShowFPS/CheckBox").ButtonPressed = showfps == "1";

        ignoreToggle = false;
    }

    public static void Fullscreen_Toggled(bool on)
    {
        if (ignoreToggle) return;
        Toggle(0, on);
        Utils.ToggleFullscreen(on);
    }

    public static void VSync_Toggled(bool on)
    {
        if (ignoreToggle) return;
        Toggle(1, on);
        Utils.ToggleVSync(on);
    }

    public static void ShowFPS_Toggled(bool on)
    {
        if (ignoreToggle) return;
        Toggle(2, on);
    }
    
    private static void Toggle(int id, bool on) {
        List<string> values = Utils.LoadSettings();
        while (values.Count <= id) values.Add("0");
        values[id] = on ? "1" : "0";
        string newValue = string.Join(":", values);
        BazookaManager.Write(BazookaManager.Settings, newValue);
    }
}
