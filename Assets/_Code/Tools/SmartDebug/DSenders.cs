namespace _Code.Tools.SmartDebug
{
  public static class DSenders
  {
    public static readonly DSender Multiplayer = new(name: "[Multiplayer]".Blue());
    public static readonly DSender Application = new(name: "[Application]".Green());
    public static readonly DSender Assets = new(name: "[Assets]".Cyan());
    public static readonly DSender GameState = new(name: "[Game State]");
    public static readonly DSender SceneData = new(name: "[Scene Data]");
    public static readonly DSender Postponer = new(name: "[Postponer]");
    public static readonly DSender Localization = new(name: "[Localization]");
  }
}