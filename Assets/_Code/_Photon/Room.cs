using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace _Code._Photon
{
    public class Room : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Players;

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(Name.text);
        }
    }
}