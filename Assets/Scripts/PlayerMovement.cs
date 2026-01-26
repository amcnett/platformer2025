using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class PlayerMovement : MonoBehaviour
{

    private Animator anim;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 10f;
    public float airControlMultiplier = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        Debug.Log(moveInput.x);
        //transform.Translate(Vector2.left * speed * Time.deltaTime); // vector is (-1, 0)
    }

    void OnJump()
    {
        jumpPressed = true;
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        //move
        float targetSpeed = moveInput.x * moveSpeed; 
        float speedDiff = targetSpeed - rb.linearVelocity.x; //how far away am i from the speed I want to be

        float accelRate = isGrounded ? moveSpeed : moveSpeed * airControlMultiplier; //if the player is in the air, we want to accelerate less
        float movement = speedDiff * accelRate;  // how hard to push the player = how wrong my current speed is times how aggressively I correct it

        rb.AddForce(Vector3.right * movement);
        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

        //jump
        if (!jumpPressed || !isGrounded)
            return;
        
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        jumpPressed = false;
        isGrounded = false;
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        Debug.Log(isGrounded);
    }

    private void OnDrawGizmos()
    {
        // Set the color of the gizmo (e.g., blue)
        Gizmos.color = Color.blue;
    
        // Draw a sphere at the center of the overlap circle with the specified radius
        // Although it is a sphere, in the 2D Scene view it will appear as a circle
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }


}
