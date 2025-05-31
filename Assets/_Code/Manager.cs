using UnityEngine;

namespace _Code
{
  public class Manager : MonoBehaviour
  {
    public static Manager Init;

    private void Awake()
    {
      if (Init == null)
      {
        
      }
      else
      {
        
      }
      
      DontDestroyOnLoad(gameObject);
    }
  }
}