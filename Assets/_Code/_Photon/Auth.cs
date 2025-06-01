using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code._Photon
{
  public class Auth : MonoBehaviourPunCallbacks
  {
    [SerializeField] private bool isDebug;

    public void AuthPlayerNickName()
    {
      int num = Random.Range(0, 1000);
      PhotonNetwork.NickName = $"Player {num}";
      
      #region Debug

      Log($"{Constants.SC_Photon} {PhotonNetwork.NickName} was connected to server");

      #endregion
    }
    
    private void Log(string msg)
    {
      Debug.Log($"[{Constants.Auth}]" + msg);
    }
  }
}