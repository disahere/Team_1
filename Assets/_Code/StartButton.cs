using Photon.Pun;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public SpawnManager spawnManager;

    public void OnButtonPressed()
    {
        if (spawnManager != null)
        {
            spawnManager.photonView.RPC("EnableMyCar", RpcTarget.AllBuffered);
        }
        else
        {
            Debug.LogWarning("SpawnManager не прив’язаний в StartButton");
        }
    }
}