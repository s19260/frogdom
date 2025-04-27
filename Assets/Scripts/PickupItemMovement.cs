using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    public float speed = 1f; // Adjust this for desired speed
    
    public float range = 1f; // Adjust this for the height of the movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        
        float y = Mathf.Sin(Time.time * speed) * range;

        // Set the sprite's position
        transform.position = new Vector3(startPosition.x, startPosition.y + y, startPosition.z);
    }
}