using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace _Code._Photon
{
    public class Create : MonoBehaviourPunCallbacks
    {
        [Header("UI")]
        public GameObject createRoom;
        public TMP_InputField roomName;
        public Slider maxPlayers;
        public TextMeshProUGUI maxPlayersText;

        private void Start()
        {
            createRoom.active = false;
        }

        public void UpdateMaxPlayers()
        {
            maxPlayersText.text = maxPlayers.value.ToString();
        }

        public void CreateRoom()
        {
            if (roomName.text != "")
            {
                Debug.Log($"Creating room {roomName.text} with max players of {maxPlayers.value}...");
                
                RoomOptions roomOptions = new RoomOptions
                {
                    MaxPlayers = (byte)maxPlayers.value,
                    IsOpen = true,
                    IsVisible = true
                };

                PhotonNetwork.CreateRoom(roomName.text, roomOptions);
            }
            else
            {
                Debug.LogError("Room name is empty");
            }
        }
    }
}