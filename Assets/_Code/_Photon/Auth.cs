using Photon.Pun;
using Photon.Realtime;
using Scenes.Nikita.Tools.SmartDebug;
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

      DLogger.Message(DSenders.SceneData)
        .WithText($"{Constants.SC_Photon} {PhotonNetwork.NickName} was connected to server")
        .WithFormat(DebugFormat.Normal)
        .Log();

      #endregion
    }
  }
}