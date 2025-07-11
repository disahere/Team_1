using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace _Code._Photon
{
    public class Join : MonoBehaviourPunCallbacks
    {
        [Header("UI")]
        public GameObject joinLobby;
        public Button joinRoomPrefab;
        public Transform roomContent;

        private Dictionary<string, GameObject> instantiatedRoomButtons = new Dictionary<string, GameObject>();

        private void Start()
        {
            joinLobby.active = false;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            HashSet<string> currentRoomNamesInList = new HashSet<string>();
            foreach (RoomInfo info in roomList)
            {
                currentRoomNamesInList.Add(info.Name);
            }
            
            List<string> roomNamesToRemove = new List<string>();
            foreach (var entry in instantiatedRoomButtons)
            {
                if (!currentRoomNamesInList.Contains(entry.Key))
                {
                    Destroy(entry.Value);
                    roomNamesToRemove.Add(entry.Key);
                }
            }
            foreach (string roomName in roomNamesToRemove)
            {
                instantiatedRoomButtons.Remove(roomName);
            }

            foreach (RoomInfo roomInfo in roomList)
            {
                if (roomInfo.RemovedFromList || !roomInfo.IsOpen || !roomInfo.IsVisible)
                {
                    continue;
                }
                
                if (instantiatedRoomButtons.TryGetValue(roomInfo.Name, out GameObject existingButtonGO))
                {
                    Room roomUIComponent = existingButtonGO.GetComponent<Room>();
                    if (roomUIComponent != null)
                    {
                        roomUIComponent.Players.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
                        Debug.Log($"Updated existing room button for: {roomInfo.Name}");
                    }
                }
                
                else
                {
                    Button newRoomButton = Instantiate(joinRoomPrefab, roomContent);
                    Room newRoomUIComponent = newRoomButton.GetComponent<Room>();

                    if (newRoomUIComponent != null)
                    {
                        newRoomUIComponent.Name.text = roomInfo.Name;
                        newRoomUIComponent.Players.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
                    }
                    
                    instantiatedRoomButtons.Add(roomInfo.Name, newRoomButton.gameObject);
                    Debug.Log($"Created new room button for: {roomInfo.Name}");
                }
            }
        }
    }
}