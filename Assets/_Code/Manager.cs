using UnityEngine;

namespace _Code
{
  public class Manager : MonoBehaviour
  {
    public static Manager Init;

    [SerializeField] private GameObject playerPrefab;
    
    private void Awake()
    {
      if (!Init)
      {
        Init = this;
      }
      else
      {
        Destroy(gameObject);
        return;
      }
      
      DontDestroyOnLoad(gameObject);
    }
  }
}