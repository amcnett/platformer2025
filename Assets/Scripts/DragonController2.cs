using UnityEngine;

public class DragonController2 : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;

    public float speed = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointA = transform.position;    
        pointB = new Vector2(transform.position.x + 5, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // PingPong gives us a value that moves 0 → 1 → 0 repeatedly
        float t = Mathf.PingPong(Time.time * speed, 1f); // Move between the two patrol points, 5 seconds
        float t = Mathf.PingPong(Time.time/(duration), 1f); // Move between the two patrol points using duration
        Debug.Log(t);
        transform.position = Vector3.Lerp(pointA, pointB, t); 
        
        // Optional: flip sprite based on direction of travel if (flipSprite && sr != null) { Vector3 dir = pointB - pointA; float direction = Mathf.Lerp(dir.x, -dir.x, t); sr.flipX = direction < 0; }
    }
}
