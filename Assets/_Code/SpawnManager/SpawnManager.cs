using _Code._Photon;
using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviourPun
{
    public Transform[] spawnPoints;
    private static readonly System.Collections.Generic.List<int> usedIndices = new();

    public static GameObject myCar { get; set; }
    private bool isCarSpawned = false;

    void Start()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom || isCarSpawned) return;

        int index = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length;

        Transform spawnPoint = spawnPoints[index];

        if (GameLoop.Instance.playerPrefab == null) return;
        string prefabName = GameLoop.Instance.playerPrefab.name;

        GameObject car = PhotonNetwork.Instantiate(prefabName, spawnPoint.position, spawnPoint.rotation);

        if (car == null) return;

        myCar = car;
        isCarSpawned = true;

        var controller = car.GetComponent<CarController>();
        if (controller != null)
            controller.enabled = false;

      
    }

    private int GetFreeSpawnIndex()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!usedIndices.Contains(i))
            {
                usedIndices.Add(i);
                return i;
            }
        }
        return -1;
    }

    [PunRPC]
    public void EnableMyCar()
    {
        if (!isCarSpawned || myCar == null) return;

        var controller = myCar.GetComponent<CarController>();
        if (controller != null)
        {
            controller.enabled = true;
            controller.canMove = false; // Початково вимкнений рух
        }
    }
}