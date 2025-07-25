using System.Collections;
using _Code.Tools.SmartDebug;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code._Photon
{
  public class GameLoop : MonoBehaviourPunCallbacks
  {
    public static GameLoop _instance { get; private set; }
    
    public static GameLoop Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = FindObjectOfType<GameLoop>();

          if (_instance == null)
          {
            GameObject go = new GameObject("GameLoop");
            _instance = go.AddComponent<GameLoop>();
          }
        }
        return _instance;
      }
    }

    void Awake()
    {
      if (_instance == null)
      {
        _instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else if (_instance != this)
      {
        Destroy(gameObject);
      }
    }
    
    public GameObject playerPrefab;

    public void PreparePlayerPrefab()
    {
      DLogger.Message(DSenders.GameState)
        .WithText($"Method PreparePlayerPrefab() was called")
        .WithFormat(DebugFormat.Normal)
        .Log();
      
      //  PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      base.OnPlayerEnteredRoom(newPlayer);
      
      DLogger.Message(DSenders.GameState)
        .WithText($"Player: [{newPlayer.NickName}] entered room".Bold())
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
      
      PhotonNetwork.LoadLevel(Constants.WackyRacers);
      
      DLogger.Message(DSenders.GameState)
        .WithText($"Loading level was completed")
        .WithFormat(DebugFormat.Normal)
        .Log();
      
      StartCoroutine(SpawnAfterDeley());
    }
    
    public IEnumerator SpawnAfterDeley()
    {
      
      
      yield return new WaitUntil(() => SpawnManager.myCar != null);

      var spawnManager = FindObjectOfType<SpawnManager>();
      if (spawnManager != null && PhotonNetwork.IsConnectedAndReady)
      {
        spawnManager.photonView.RPC("EnableMyCar", RpcTarget.AllBuffered);
      }
      
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