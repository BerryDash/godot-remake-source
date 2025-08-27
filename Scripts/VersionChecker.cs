using Godot;
using System;
using System.Text;
using System.Collections.Generic;

public partial class VersionChecker : Control {
    public override void _Ready() {
        Label VersionLabel = GetNode<Label>("Version");
        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        VersionLabel.Text = "Version: " + GameVersion;

        HttpRequest GetLatestVersionNode = GetNode<HttpRequest>("LatestVersion/HTTPRequest");

        GetLatestVersionNode.RequestCompleted += OnRequestCompleted;

        string url = "https://berrydash-godot.lncvrt.xyz/database/getLatestVersion.php?version=" + GameVersion;

        GetLatestVersionNode.Request(url);
    }

    private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body) {
        string ResponseBody = Encoding.UTF8.GetString(body);
        GD.Print(ResponseBody);

        List<string> ResponseValues = [.. ResponseBody.Split(";")];

        Globals.AccessLevel = ResponseValues[0];
        Globals.LatestVersion = ResponseValues[1];

        Label LatestVersionLabel = GetNode<Label>("LatestVersion");

        LatestVersionLabel.Text = "Latest Version: " + Globals.LatestVersion;
    }
}
