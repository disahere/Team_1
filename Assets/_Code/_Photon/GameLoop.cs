using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code._Photon
{
  public class GameLoop : MonoBehaviourPunCallbacks
  {
    [SerializeField] private GameObject playerPrefab;

    public void PreparePlayerPrefab()
    {
      Log("Method PreparePlayerPrefab() was called");
      PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      base.OnPlayerEnteredRoom(newPlayer);
      Log($"Player: [{newPlayer.NickName}] entered room");
    }

    public override void OnJoinedRoom()
    {
      base.OnJoinedRoom();
      Log("Joined room");

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
      Log($"Player: [{otherPlayer.NickName}] left room");
    }

    private void Log(string msg)
    {
      Debug.Log($"[{Constants.GameLoop}]" + msg);
    }
  }
}