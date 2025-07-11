using UnityEngine;

namespace Movement
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;

        [SerializeField] private Transform orientation;
        
        private float yRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            
            yRotation += mouseX;
            
            
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}