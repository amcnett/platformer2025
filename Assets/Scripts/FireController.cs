using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 8f;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        direction = player.GetComponent<PlayerController>().GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.right * force);
        //rb.velocity = force * Vector2.right; //need to get direction from the player
        rb.velocity = force * direction;
        Invoke("Die", 4f); // wait 4 seconds before destroying the bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
            Die();
    }
}
