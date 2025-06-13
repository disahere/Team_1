using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        Debug.Log(transform.position);
        transform.position = cameraPosition.position;
        if (transform.position == cameraPosition.position)
        {
            Debug.Log("Camera moved");
        }
    }
}