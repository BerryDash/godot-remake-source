using Godot;
using System;

public partial class Globals : Node {
    public static string LatestVersion = "";
    public static string AccessLevel = "";
    public static bool VersionChecked = false;

    public static float MovementSpeed = 800f;
    public static float DefaultMovementSpeed = MovementSpeed;
    public static float JumpHeight = -0.5f;
    public static float DefaultJumpHeight = JumpHeight;
    public static float Gravity = 0.0005f;
    public static float DefaultGravity = JumpHeight;

    public static string ActiveEffect = "none";
    public static float EffectTimer = 0f;

    public static int Score = 0;
    public static int HighScore = 0;
    public static bool IsPaused = false;
}
