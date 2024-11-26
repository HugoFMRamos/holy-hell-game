using UnityEngine;

public class Climbing : MonoBehaviour {

    [Header("References")]
    public Transform orientation;
    public LayerMask whatIsWall;
    public Rigidbody rb;
    public PlayerController pc;

    [Header("Climbing")]
    public float climbSpeed;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private void Update() {
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();

        if (pc.grounded && climbing)
        {
            StopClimbing();
        }
    }

    private void StateMachine() {
        // State 1 - Climbing
        if (wallFront && wallLookAngle < maxWallLookAngle) {
            if (!climbing) StartClimbing();
        }

        // State 3 - None
        else {
            if (climbing) StopClimbing();
        }
    }

    private void WallCheck() {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement()
    {
        // Check if player is pressing the "W" or "S" key
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            float moveDirection = Input.GetAxisRaw("Vertical");
            // Apply climbing movement
            rb.velocity = new Vector3(rb.velocity.x, climbSpeed * moveDirection, rb.velocity.z);
        }
        else
        {
            // Stop vertical movement to keep the player stuck to the wall
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }


    private void StopClimbing()
    {
        climbing = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
