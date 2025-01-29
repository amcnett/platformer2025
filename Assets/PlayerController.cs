using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private
    private Vector2 movementVector;
    private Animator animator;                                // for animation
    private SpriteRenderer sprite_r;                          // for sprite flip

    //public
    public float speed = 3;
    public float jumpForce = 250;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();                 // for animation
        sprite_r = GetComponent<SpriteRenderer>();           // for sprite flip
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(movementVector.x));   // for animation
        if (movementVector.x > 0)
            transform.Translate(Vector2.right * speed * Time.deltaTime);  // vector is (1,0)
        else if (movementVector.x < 0)
            transform.Translate(Vector2.left * speed * Time.deltaTime); // vector is (-1, 0)

        if (movementVector.x < 0) // if we are walking to the left
            sprite_r.flipX = true;
        else if (movementVector.x > 0) //if we are walking to the right
            sprite_r.flipX = false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        Debug.Log("Jumping!!!");
        transform.Translate(Vector2.up * jumpForce * Time.deltaTime);
    }
}
