using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.right * force);
        rb.velocity = force * Vector2.right;
        Invoke("Die", 4f); // wait 4 seconds before destroying the bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
