using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace _Code._Photon
{
    public class Join : MonoBehaviourPunCallbacks
    {
        [Header("UI")]
        public GameObject joinLobby;
        public Button joinRoomPrefab;

        private void Start()
        {
            joinLobby.active = false;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                Debug.Log($"Room Name: {roomList[i].Name}, Max Players: {roomList[i].MaxPlayers}");
            }
        }

        public void JoinRoom()
        {
            
        }
    }
}   