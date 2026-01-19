using System.Collections.Generic;
using System.Linq;
using Godot;

public class Utils {
    public static void ToggleFullscreen(bool on, List<string> settings)
    {
        DisplayServer.WindowSetMode(on ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Maximized);

        string vsync = settings.ElementAtOrDefault(1) ?? "1";
        ToggleVSync(vsync == "1");
    }

    public static void ToggleFullscreen(bool on)
    {
        List<string> settings = LoadSettings();
        ToggleFullscreen(on, settings);
    }

    public static void ToggleVSync(bool on)
    {
        DisplayServer.WindowSetVsyncMode(on ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
    }

    public static List<string> LoadSettings() {
        string value = BazookaManager.Read(BazookaManager.Settings, "");
        List<string> values = [.. value.Split(':')];
        return values;
    }
}
