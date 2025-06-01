using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace _Code._Photon
{
  public class Connection : MonoBehaviourPunCallbacks
  {
    [SerializeField] private Auth auth;
    [SerializeField] private bool isDebug;

    private void Awake()
    {
      #region Debug

      if (isDebug)
        Log($"{Constants.SC_Photon} Try to start server...");

      #endregion

      PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
      PhotonNetwork.ConnectUsingSettings();
      PhotonNetwork.GameVersion = "1";
    }

    public override void OnConnectedToMaster()
    {
      base.OnConnectedToMaster();
      auth.AuthPlayerNickName();
      
      if(PhotonNetwork.IsMasterClient)
        auth.PreparePlayerPrefab();

      #region Debug

      if (isDebug)
        Log($"{Constants.SC_Photon} Server was started");

      #endregion
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
      Log($"Player: [{PhotonNetwork.NickName}] was disconnected by reason: {cause}");
    }

    public override void OnJoinedRoom()
    {
      #region Debug

      if (isDebug)
        Log($"{Constants.SC_Photon} Joined the room");

      #endregion

      PhotonNetwork.LoadLevel(Constants.MenuScene);
    }

    public override void OnCreatedRoom()
    {
      base.OnCreatedRoom();
      Log("Room was successfully created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
      base.OnCreateRoomFailed(returnCode, message);
      Log($"Failed to create a room by reason: {message}\n {returnCode}");
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
      Debug.Log($"[{Constants.Connection}]" + msg);
    }
  }
}