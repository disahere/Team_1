using System;
using _Code._Photon;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.UI
{
    public class ColorChoose : MonoBehaviour
    {
        [Header("--- UI ---")]
        public Image check; //An indicator of a selected color

        [Header("--- Car ---")]
        public GameObject colorCar; //Color chosen by a specific button (e.g., red button selects a red car)
        public GameObject defaultCar; //Default color that is applied to the player's car color if they don't choose any
        public GameObject showcaseCar; //Car that is shown in the main menu

        private Material chosenCarMaterial; //Material with the color that players see on the car, used to change showcase car color in the main menu
        private Material showcaseCarMaterial; //Showcase car's material that is set by that one ^^

        private Transform chosenBodyTransform; //Body of the car's prefab which has material needed to change showcase car
        private Transform showcaseBodyTransform; //Same thing, different car
        
        private void Start()
        {
            chosenBodyTransform = colorCar.transform.Find("Body"); //Finds a Body component in the car's prefab
            showcaseBodyTransform = showcaseCar.transform.Find("Body"); //Same thing
            
            chosenCarMaterial = chosenBodyTransform.GetComponent<MeshRenderer>().sharedMaterial; //Sets the material of the car which is its color
            
            check.enabled = false; //Disable all checkmarks
            
            GameLoop.Instance.playerPrefab = defaultCar; //Sets the player's car color instantly if the player does not choose it
        }

        //Sets the player's car color
        public void SelectColor()
        {
            GameLoop.Instance.playerPrefab = colorCar; //Sets the chosen color as the player's color
            
            showcaseBodyTransform.GetComponent<MeshRenderer>().material = chosenCarMaterial; //Sets the showcase car's color to the same as the player's chosen color
            
            check.enabled = true; //Shows a checkmark indicating the selection
        }

        //Disables the previous checkmark when the player chooses another color
        public void DeselectColor()
        {
            check.enabled = false;
        }
    }
}