using _Code._Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        LapTracker lapTracker = car.GetComponent<LapTracker>();
        if (lapTracker != null)
        {
            GameObject panel = GameObject.Find("FinishPanel");
            lapTracker.finishPanel = panel;

            GameObject lapTextObj = GameObject.Find("LapCounterText");
            if (lapTextObj != null)
                lapTracker.lapCounterText = lapTextObj.GetComponent<TMP_Text>();
        }
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

        var camera = myCar.GetComponentInChildren<Camera>();
        var controller = myCar.GetComponent<CarController>();
        if (controller != null && camera !=null)
        {
            controller.enabled = true;
            controller.canMove = false; // Початково вимкнений рух
            camera.enabled = true;
        }
    }
}