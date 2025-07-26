using UnityEngine;
using TMPro;
using Photon.Pun;

public class LapTracker : MonoBehaviourPun //TODO: Додати чекпоінти для перевірки кругів
{
    public int totalLaps = 3;
    private int currentLap = 0;
    private bool canCount = true;
    private float lapCooldown = 2f;

    public int _checkpoints;
    private int _checkpointscollected;

    public GameObject finishPanel;      // Панель фінішу
    public TMP_Text lapCounterText;     // Текст UI

    // Встановити тег LapTrigger на триггері трасі

    private void Start()
    {
        if (finishPanel != null)
            finishPanel.SetActive(false);

        UpdateLapUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_checkpoints > _checkpointscollected)
            return;
            
        // Перевіряємо, чи це машина (перевірка на MeshRenderer)
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        if (mr == null) return;

        // Далі перевірка, чи це машина локального гравця
        PhotonView pv = other.GetComponentInParent<PhotonView>();
        if (pv == null || !pv.IsMine) return;

        // Перевірка тегу триггера — це потрібно на самому об'єкті з колайдером
        if (!canCount) return;

        currentLap++;
        UpdateLapUI();

        if (currentLap >= totalLaps)
        {
            if (finishPanel != null)
                finishPanel.SetActive(true);

            // Можна додати додаткову логіку (стоп машини і т.д.)
        }
        else
        {
            StartCoroutine(LapCooldown());
        }
    }

    private System.Collections.IEnumerator LapCooldown()
    {
        canCount = false;
        yield return new WaitForSeconds(lapCooldown);
        canCount = true;
    }

    private void UpdateLapUI()
    {
        if (lapCounterText != null)
            lapCounterText.text = $"Lap: {currentLap} / {totalLaps}";
    }
}