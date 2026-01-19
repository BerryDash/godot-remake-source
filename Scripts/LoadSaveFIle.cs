using Godot;

public partial class LoadSaveFIle : Control {
	public override void _Ready() {
		Globals.HighScore = int.Parse(BazookaManager.Read(BazookaManager.HighScore, "0"));
	}
}
