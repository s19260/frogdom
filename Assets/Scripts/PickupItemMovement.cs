using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    public float speed = 1f;
    public float range = 1f; 

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(startPosition.x, startPosition.y + y, startPosition.z);
    }
}