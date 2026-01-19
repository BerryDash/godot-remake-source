using Godot;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class FirstLoad : Control {
    public override void _Ready() {
        Label VersionLabel = GetNode<Label>("VersionText/Version");
        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        VersionLabel.Text = "Version: " + GameVersion;

        if (Globals.FirstLoadDone) {
            Label LatestVersionLabel = GetNode<Label>("VersionText/LatestVersion");

            LatestVersionLabel.Text = "Latest Version: " + Globals.LatestVersion;
        } else {
            //Load settings
            List<string> settings = Utils.LoadSettings();
            string fullscreen = settings.ElementAtOrDefault(0) ?? "1";
            Utils.ToggleFullscreen(fullscreen == "1", settings); //This will also load VSync

            //Version checking
            HttpRequest GetLatestVersionNode = GetNode<HttpRequest>("VersionText/LatestVersion/HTTPRequest");

            GetLatestVersionNode.RequestCompleted += OnRequestCompleted;

            GetLatestVersionNode.Request("https://games.lncvrt.xyz/api/can-load-client", [
                "Requester: BerryDashGodotClient",
                "ClientVersion: " + GameVersion,
                "ClientPlatform: " + OS.GetName()
            ]);
        }
    }

    private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body) {
        string ResponseBody = Encoding.UTF8.GetString(body);
        GD.Print(ResponseBody);

        List<string> ResponseValues = [.. ResponseBody.Split(";")];

        Globals.AccessLevel = ResponseValues[0];
        Globals.LatestVersion = ResponseValues[1];

        Label LatestVersionLabel = GetNode<Label>("VersionText/LatestVersion");

        LatestVersionLabel.Text = "Latest Version: " + Globals.LatestVersion;

        Globals.FirstLoadDone = true;

        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        if (Globals.AccessLevel != "all" || GameVersion != Globals.LatestVersion) {
            var error = GetTree().ChangeSceneToFile("res://Scenes/OutdatedVersion.tscn");

            if (error != Error.Ok) {
                GD.PrintErr("Failed to change scene: " + error);
            }
        }
    }
}
