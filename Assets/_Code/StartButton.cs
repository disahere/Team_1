using UnityEngine;
using UnityEngine.UI;

public class StartRaceButton : MonoBehaviour
{
    public Button startButton; // Тільки кнопка в інспекторі

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    private void OnStartButtonPressed()
    {
        // Автоматично шукає машину (CarController на сцені)
        CarController controller = FindObjectOfType<CarController>();
        if (controller != null)
        {
            controller.StartDriving();
            Debug.Log("canMove стало true, машина готова їхати!");
        }
        else
        {
            Debug.LogWarning("CarController не знайдено на сцені!");
        }

        startButton.gameObject.SetActive(false); // Сховати кнопку
    }
}