using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool facingRight = true;
    private bool jumpPressed = false;
    private bool isGrounded = true;

    [Tooltip("How fast the player should move.")]
    public float moveSpeed = 8f;
    public float jumpForce = 10f;
    public float airMultiplier = 0.25f;
    public float fallGravityScale = 2f; // Normal gravity scale (200%)

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = .25f;
    public Transform shadowDot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        Debug.Log("Player movement: " + moveInput.x);
    }

    void OnJump(InputValue movementValue)
    {
        jumpPressed = true;
        CheckGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        //one way to move game objects = by using translate
        //transform.Translate(moveInput * Vector2.left * Time.deltaTime);
        if (moveInput.x < 0 && facingRight) //moving left but facing right
        {
            Flip();
            facingRight = false;
        }
        else if (moveInput.x > 0 && !facingRight)  //moving right but facing left
        {
            Flip();
            facingRight = true;
        }

        if(isGrounded)
            shadowDot.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput.x * moveSpeed; //how fast I want the player to go
        float speedDiff = targetSpeed - rb.linearVelocity.x; //how far away am I from the speed I want to be
        float accelRate = isGrounded ? moveSpeed : moveSpeed * airMultiplier;
        float movement = speedDiff * accelRate; //how hard to push the player

        rb.AddForce(Vector2.right * movement);
        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpPressed = false;
            isGrounded = false;
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravityScale;
        }
        else
        {
            rb.gravityScale = 1f; //default (100%)
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 100f, groundLayer);
        if (hit)
        {
            shadowDot.position = hit.point;
            shadowDot.gameObject.SetActive(true);
        }

    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1; // inverts the x value of the scale
        transform.localScale = theScale; //set game object scale equal to our modified scale
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
