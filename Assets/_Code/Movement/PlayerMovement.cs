using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("--- Movement ---")]
    public float movementSpeed;
    public float groundDrag;
    
    [Header("--- Jump ---")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump = true;
    
    [Header("--- Sprinting ---")]
    public float sprintMultiplier;
    private float currentMovementSpeed;
    private bool isSprinting;
    
    [Header("--- Keybinds ---")]
    public KeyCode jumpKey;
    public KeyCode sprintKey;
    
    [Header("--- Ground Check ---")] 
    public float playerHeight;
    public bool isGrounded;
    public float distanceConstant;
    
    [SerializeField] private Transform orientation;
    
    private float horizontalInput;
    private float verticalInput;
    
    private Vector3 moveDirection;
    private Vector3 rawMoveInput;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + distanceConstant);
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = groundDrag;
        }
        
        PlayerInput();
        SpeedControl();
    }
    
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rawMoveInput = new Vector3(horizontalInput, 0f, verticalInput);

        if (Input.GetKeyDown(jumpKey) && isGrounded && canJump)
        {
            canJump = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        if (Input.GetKey(sprintKey) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            currentMovementSpeed = movementSpeed * sprintMultiplier;
            isSprinting = true;
        }
        else
        {
            currentMovementSpeed = movementSpeed;
            isSprinting = false;
        }
    }
    
    private void MovePlayer()
    {
        moveDirection = (orientation.forward * rawMoveInput.z) + (orientation.right * rawMoveInput.x);

        bool hasInput = rawMoveInput.magnitude > 0.01f;

        if (isGrounded)
        {
            if (hasInput)
            {
                Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                
                if (flatVelocity.magnitude < currentMovementSpeed)
                {
                    rb.AddForce(moveDirection.normalized * currentMovementSpeed * 10f, ForceMode.Force);
                }
            }
            else
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }
        else
        {
            if (hasInput)
            {
                Vector3 airHorizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

                float airSpeedLimit = movementSpeed * airMultiplier;

                if (airHorizontalVelocity.magnitude < airSpeedLimit)
                {
                    rb.AddForce(moveDirection.normalized * airSpeedLimit * 10f, ForceMode.Acceleration);
                }
            }
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        float currentMaxAllowedSpeed = currentMovementSpeed;

        if (flatVelocity.magnitude > currentMaxAllowedSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * currentMaxAllowedSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
