using Photon.Pun;
using UnityEngine.UI;

namespace _Code
{
  public class StartRaceButton : MonoBehaviourPunCallbacks
  {
    public Button readyButton;
    public int minPlayerToStartGame = 2;
    public bool isLocalTesting;

    private int _readyPlayers;

    private void Start()
    {
      readyButton.onClick.AddListener(OnReadyButtonPressed);
    }

    private void OnReadyButtonPressed()
    {
      _readyPlayers++;
      if (isLocalTesting || minPlayerToStartGame <= _readyPlayers)
        StartRace();
    }

    private void StartRace()
    {
      if (SpawnManager.myCar != null && SpawnManager.myCar.GetComponent<PhotonView>().IsMine)
      {
        var controller = SpawnManager.myCar.GetComponent<CarController>();
        if (controller != null)
        {
          controller.canMove = true;
        }
        // else
        //   controller.canMove = true;
        
        readyButton.gameObject.SetActive(false);
      }
    }
  }
}