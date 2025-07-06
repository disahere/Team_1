using _Code.Tools.SmartDebug;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace _Code._Photon
{
  public class NickNameChanger : MonoBehaviourPunCallbacks
  {
    [SerializeField] private TMP_InputField playerNickName;

    public void ChangeName()
    {
      if (playerNickName)
      {
        PhotonNetwork.NickName = playerNickName.text;
        
        DLogger.Message(DSenders.SceneData)
          .WithText($"{Constants.SC_Photon} Player nickname was changed: [{PhotonNetwork.NickName}] ")
          .WithFormat(DebugFormat.Normal)
          .Log();
      }
      else
      {
        DLogger.Message(DSenders.SceneData)
          .WithText($"{Constants.SC_Photon} Player nickname didn't changed: [{PhotonNetwork.NickName}] ")
          .WithFormat(DebugFormat.Normal)
          .Log();
      }
    }
  }
}