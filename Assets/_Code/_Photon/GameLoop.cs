using System.Collections;
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
      
      PhotonNetwork.LoadLevel(Constants.DarkBusiness);
      Log("Loading level was completed");
      
      StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
      yield return new WaitForSeconds(1f);

      if (PhotonNetwork.IsMasterClient)
      {
        Log("Local client was found");
        var camera = playerPrefab.GetComponentInChildren<Camera>();
        if (camera)
        {
          Log("Camera was founded!");
          camera.enabled = true;
          // camera.gameObject.SetActive(true);
        }
        else
        {
          Log("Camera in children is null");
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
      Log($"Player: [{otherPlayer.NickName}] left room");
    }

    private void Log(string msg)
    {
      Debug.Log($"[{Constants.GameLoop}]" + msg);
    }
  }
}