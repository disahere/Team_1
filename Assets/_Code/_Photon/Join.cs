using Photon.Pun;

namespace _Code._Photon
{
  public class Join : MonoBehaviourPunCallbacks
  {
    public void JoinRoom()
    {
      PhotonNetwork.JoinRandomRoom();
    }
  }
}