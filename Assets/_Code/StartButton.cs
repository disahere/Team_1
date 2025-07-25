using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartRaceButton : MonoBehaviourPunCallbacks
{
    public Button startButton; // Прив'язується у інспекторі

    private void Start()
    {
        // Якщо гравець не перший (не MasterClient), ховаємо кнопку
        if (!PhotonNetwork.IsMasterClient)
        {
            startButton.gameObject.SetActive(false);
            return;
        }

        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    private void OnStartButtonPressed()
    {
        if (SpawnManager.myCar != null && SpawnManager.myCar.GetComponent<PhotonView>().IsMine)
        {
            var controller = SpawnManager.myCar.GetComponent<CarController>();
            if (controller != null)
            {
                controller.canMove = true;
                Debug.Log("✅ Машина може рухатися");
            }
        }
        else
        {
            Debug.LogWarning("❌ Машина не знайдена або не твоя");
        }

        startButton.gameObject.SetActive(false);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        // Якщо MasterClient змінюється під час гри, сховати/показати кнопку відповідно
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
}