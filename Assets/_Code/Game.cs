using System;
using System.Collections;
using _Code._Photon;
using Scenes.Nikita.Tools.SmartDebug;
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
      DLogger.Message(DSenders.GameState)
        .WithText($"{Constants.SC_Game} Init load menu was done".Bold().Green())
        .WithFormat(DebugFormat.Normal)
        .Log();
    }
  }
}