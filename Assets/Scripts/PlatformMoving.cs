using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float speed;
    private int direction = 1;
    public float maxDisplacement = 1;
    private float currentDisplacement = 0;
    private GameObject player;
    private bool isPlayerStanding = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float xDisplacement = direction * speed * Time.deltaTime;
        Vector3 displacementVector = new Vector3(xDisplacement, 0, 0);
        transform.Translate(displacementVector);

        currentDisplacement = currentDisplacement + xDisplacement;
        if (currentDisplacement >= maxDisplacement)
        {
            direction = -1;
        }

        if (currentDisplacement <= 0)
        {
            direction = 1;
        }

        if (isPlayerStanding)
        {
            player.transform.Translate(displacementVector);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerStanding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerStanding = false;
        }
    }
}
