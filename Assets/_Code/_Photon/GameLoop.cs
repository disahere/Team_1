using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using Scenes.Nikita.Tools.SmartDebug;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code._Photon
{
  public class GameLoop : MonoBehaviourPunCallbacks
  {
    [SerializeField] private GameObject playerPrefab;

    public void PreparePlayerPrefab()
    {
      DLogger.Message(DSenders.GameState)
        .WithText($"Method PreparePlayerPrefab() was called")
        .WithFormat(DebugFormat.Normal)
        .Log();
      
      PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      base.OnPlayerEnteredRoom(newPlayer);
      
      DLogger.Message(DSenders.GameState)
        .WithText($"Player: [{newPlayer.NickName}] entered room".Bold().Green())
        .WithFormat(DebugFormat.Normal)
        .Log();
    }

    public override void OnJoinedRoom()
    {
      base.OnJoinedRoom();
      DLogger.Message(DSenders.GameState)
        .WithText($"Joined room")
        .WithFormat(DebugFormat.Normal)
        .Log();
      
      PhotonNetwork.LoadLevel(Constants.DarkBusiness);
      
      DLogger.Message(DSenders.GameState)
        .WithText($"Loading level was completed")
        .WithFormat(DebugFormat.Normal)
        .Log();
      
      StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
      yield return new WaitForSeconds(1f);

      if (PhotonNetwork.IsMasterClient)
      {
        
        DLogger.Message(DSenders.Multiplayer)
          .WithText($"Local client was found".Bold())
          .WithFormat(DebugFormat.Normal)
          .Log();

        var camera = playerPrefab.GetComponentInChildren<Camera>();
        if (camera)
        {
          DLogger.Message(DSenders.Multiplayer)
            .WithText($"Camera was founded!".Green())
            .WithFormat(DebugFormat.Normal)
            .Log();
            
          camera.enabled = true;
          // camera.gameObject.SetActive(true);
        }
        else
        {
          DLogger.Message(DSenders.Multiplayer)
            .WithText($"Camera in children is null")
            .WithFormat(DebugFormat.Exception)
            .Log();
        }
      }

      PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public void Leave()
    {
      PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
      base.OnLeftRoom();
      SceneManager.LoadScene(Constants.MenuScene);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
      base.OnPlayerLeftRoom(otherPlayer);
      DLogger.Message(DSenders.Multiplayer)
        .WithText($"Player: [{otherPlayer.NickName}] left room".Bold())
        .WithFormat(DebugFormat.Normal)
        .Log();
    }
  }
}