using Photon.Pun;
using UnityEngine;

namespace _Code._Photon
{
  public class Auth : MonoBehaviourPunCallbacks
  {
    [SerializeField] private bool isDebug;
    [SerializeField] private GameObject playerPrefab;

    public void AuthPlayerNickName()
    {
      int num = Random.Range(0, 1000);
      PhotonNetwork.NickName = $"Player {num}";
      
      #region Debug

      Log($"{Constants.SC_Photon} {PhotonNetwork.NickName} was connected to server");

      #endregion
    }

    public void PreparePlayerPrefab()
    {
      PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    
    private void Log(string msg)
    {
      Debug.Log($"[{Constants.Auth}]" + msg);
    }
  }
}