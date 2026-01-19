using Godot;

public partial class Globals : Node {
    public static string LatestVersion = "";
    public static string AccessLevel = "";
    public static bool FirstLoadDone = false;

    public static float MovementSpeed = 800f;
    public static float DefaultMovementSpeed = MovementSpeed;
    public static float BerrySpeed = 500f;
    public static float DefaultBerrySpeed = BerrySpeed;
    public static float JumpHeight = -1100f;
    public static float DefaultJumpHeight = JumpHeight;
    public static float Gravity = 3000f;
    public static float DefaultGravity = Gravity;

    public static string ActiveEffect = "none";
    public static float EffectTimer = 0f;

    public static int Score = 0;
    public static int HighScore = 0;
    public static bool IsPaused = false;

    public static float SoundEffectVolume = 100f;
}
