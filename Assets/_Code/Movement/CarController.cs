using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput; // Stores the input from the horizontal axis (A/D keys or left/right arrow keys), used for steering
    private float verticalInput;   // Stores the input from the vertical axis (W/S keys or up/down arrow keys), used for acceleration/braking
    private float currentSteerAngle; // Stores the calculated steering angle for the front wheels
    private float currentbreakForce; // Stores the calculated brake force to apply to the wheels
    private bool isBreaking;         // A boolean flag that is true when the brake is pressed

    [Header("Settings")]
    public float motorForce;    // The maximum torque applied to the motor wheels for acceleration
    public float breakForce;    // The maximum torque applied to all wheels when braking
    public float maxSteerAngle; // The maximum angle the front wheels can turn left or right

    public bool canMove = false;
    
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    [Header("Wheels")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    
    void Update()
    {
        if (!canMove)
            return;

        // рух машини
    }
    
    // Gathers input from the player for horizontal (steering), vertical (acceleration/reverse) and braking actions
    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        
        verticalInput = Input.GetAxis("Vertical");
        
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // Manages the application of motor torque for acceleration/deceleration and sets the current brake force based on input
    private void HandleMotor() {
        // Apply motor torque to the front wheels based on vertical input and motorForce
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        
        // if 'isBreaking' is true, set 'currentbreakForce' to 'breakForce', otherwise set it to 0
        currentbreakForce = isBreaking ? breakForce : 0f;
        currentbreakForce = isBreaking ? breakForce : 0f;
        
        ApplyBreaking(); // Call the separate method to apply the calculated brake force to all wheels
    }

    // Applies the calculated 'currentbreakForce' to all four wheel colliders
    private void ApplyBreaking() {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    // Manages the steering of the front wheels based on horizontal input
    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput; // Calculate the target steer angle

        // Apply the calculated steer angle to the front wheel colliders
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    // Updates the position and rotation of the visual 3D wheel models to match the physics state of their corresponding WheelColliders
    private void UpdateWheels() {
        UpdateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheel(frontRightWheelCollider, frontRightWheelTransform);
        
        UpdateWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    // A method to get the world position and rotation from a WheelCollider
    //and apply it to a visual wheel's Transform
    //This makes the visual wheel accurately represent the physics wheel's state (rotation, suspension, steering)
    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}