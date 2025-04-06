using TMPro;
using UnityEngine;

namespace CodeBase
{
  public class Counter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int counter = 0;

    public void Add()
    {
      counter++;
      UpdateUI();
    }

    public void Dis()
    {
      counter--;
      UpdateUI();
    }

    private void UpdateUI() => 
      counterText.text = counter.ToString();
  }
}