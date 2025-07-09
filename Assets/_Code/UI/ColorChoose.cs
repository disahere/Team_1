using System;
using _Code._Photon;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.UI
{
    public class ColorChoose : MonoBehaviour
    {
        [Header("--- UI ---")]
        public Image check;

        [Header("--- Car ---")]
        public GameObject colorCar;
        public GameObject defaultCar;

        private void Start()
        {
            check.enabled = false;
            
            GameLoop.Instance.playerPrefab = defaultCar;
        }

        public void SelectColor()
        {
            GameLoop.Instance.playerPrefab = colorCar;
            
            check.enabled = true;
            Debug.Log("Color was selected");
        }

        public void DeselectColor()
        {
            check.enabled = false;
        }
    }
}