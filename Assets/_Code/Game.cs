using System;
using System.Collections;
using _Code._Photon;
using UnityEngine;

namespace _Code
{
  public class Game : MonoBehaviour
  {
    [Header("Photon Connection")]
    [SerializeField] private Connection connection;

    private void Start()
    {
      StartCoroutine(InitLoadMenu());
    }

    private IEnumerator InitLoadMenu()
    {
      yield return new WaitForSeconds(Constants.InitTime);
      
      connection.OnJoinedRoom();
      Log($"{Constants.SC_Game} Init load menu was done");
    }
    
    private void Log(string msg)
    {
      Debug.Log($"[{Constants.Game}]" + msg);
    }
  }
}