namespace Scenes.Nikita.Tools.SmartDebug
{
  public class DSender
  {
    public readonly string Name;
    
    public DebugPlatform Platform = DebugPlatform.All;

    public DSender(string name) =>
      Name = name;
  }
}