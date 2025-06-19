using UnityEngine;

namespace Movement
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;

        [SerializeField] private Transform orientation;
        
        private float xRotation = 0f;
        private float yRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            xRotation -= mouseY;
            yRotation += mouseX;
            
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}