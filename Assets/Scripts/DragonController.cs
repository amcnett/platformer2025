using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private int direction = 1; //facing right
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0,-3), Color.red, 0.5f);
        //Debug.Log(hit);
        if (hit.collider == null) //no hit with raycasting
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;  //make dragon face direction of destination
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);

        RaycastHit2D headHit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if (headHit.collider != null) //if hit was detected
        {
            Debug.Log(headHit.collider.gameObject);
            if (headHit.collider.gameObject.CompareTag("player"))
            {
                Destroy(gameObject); //destroy the dragon.... if you said "this" it just destroys the script component
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.CompareTag("fire"))
            Destroy(gameObject);
    }
}
