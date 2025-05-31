using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Code._Photon
{
  public class Connection : MonoBehaviourPunCallbacks
  {
    [SerializeField] private bool isDebug;

    private void Awake()
    {
      #region Debug

      if (isDebug)
        Log($"{Constants.Connection} Try to start server...");

      #endregion

      PhotonNetwork.AutomaticallySyncScene = true;

      int num = Random.Range(0, 1000);
      PhotonNetwork.NickName = $"Player {num}";

      #region Debug

      Log($"{Constants.Connection} {PhotonNetwork.NickName} was connected to server");

      #endregion
    }

    private void Start()
    {
      PhotonNetwork.ConnectUsingSettings();
      PhotonNetwork.GameVersion = "1";
    }

    public override void OnConnectedToMaster()
    {
      base.OnConnectedToMaster();

      #region Debug

      if (isDebug)
        Log($"{Constants.Connection} Server was started");

      #endregion
    }

    public override void OnJoinedRoom()
    {
      #region Debug

      if (isDebug)
        Log($"{Constants.Connection} Joined the room");

      #endregion

      PhotonNetwork.LoadLevel(Constants.MenuScene);
    }

    public void CreateRoom()
    {
      RoomOptions roomOptions = new RoomOptions
      {
        MaxPlayers = 20,
        IsOpen = true,
        IsVisible = true
      };

      PhotonNetwork.CreateRoom("Main", roomOptions);
    }

    public void JoinRoom()
    {
      PhotonNetwork.JoinRandomRoom();
    }

    private void Log(string msg)
    {
      Debug.Log(msg);
    }
  }
}