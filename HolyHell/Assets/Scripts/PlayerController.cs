using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;

    private float moveSpeed;
    [Header("Movement")]
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float radius;
    private float startYScale;
    bool cantStand;

    [HideInInspector] public float currentSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public MovementState state;
    public enum MovementState {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start() {
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        radius = capsuleCollider.radius;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update() {
        //Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        cantStand = Physics.CheckCapsule(transform.position , transform.position + new Vector3(0, playerHeight * 0.25f + 0.3f, 0), radius, whatIsGround);

        GetInput();
        SpeedControl();
        StateHandler();

        //Handle Drag
        if(grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0f;
        }

        currentSpeed = rb.velocity.magnitude;
    }
    
    private void FixedUpdate() {
        MovePlayer();    
    }

    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Crouch
        if(Input.GetKeyDown(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            playerHeight = 1f;
            rb.AddForce(Vector3.down * 20f, ForceMode.Impulse);
        }

        if(!Input.GetKey(crouchKey) && !cantStand) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            playerHeight = 2f;
        }
    }

    private void StateHandler() {
        //Crouching
        if(Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        //Sprinting
        else if(grounded && !cantStand && Input.GetKey(sprintKey)) {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        //Walking
        else if(grounded && !cantStand) {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        //Air
        else {
            state = MovementState.air;
        }
    }

    private void MovePlayer() {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Check if the player is on a slope
        if (OnSlope() && !exitingSlope) {
            // Adjust the move direction to be along the slope
            Vector3 slopeMoveDirection = GetSlopeMoveDirection();

            // Apply force along the slope
            rb.AddForce(slopeMoveDirection * moveSpeed * 20f, ForceMode.Force);

            // Prevent upward movement if the player is trying to climb
            if (rb.velocity.y > 0) {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
        } 
        else {
            // Ground movement
            if (grounded) {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            // Air movement
            else if (!grounded) {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        // Use gravity unless on a slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl() {
        if (OnSlope() && !exitingSlope) {
            // Limit speed on slopes
            Vector3 slopeVelocity = Vector3.ProjectOnPlane(rb.velocity, slopeHit.normal);
            if (slopeVelocity.magnitude > moveSpeed) {
                rb.velocity = slopeVelocity.normalized * moveSpeed;
            }
        } 
        else {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit flat movement speed
            if (flatVelocity.magnitude > moveSpeed) {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    private void Jump() {
        exitingSlope = true;

        //Reset Y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection() {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
