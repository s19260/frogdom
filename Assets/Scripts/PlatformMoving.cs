using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float speed;

    //  private Rigidbody2D rb;
    private int direction = 1;
    public float maxDisplacement = 1;
    private float currentDisplacement = 0;

    private GameObject player;
    private bool isPlayerStanding = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        // rb = GetComponent<Rigidbody2D>();
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
            Debug.Log("collision enter with player");
            isPlayerStanding = true;
            // player.GetComponent<PlayerMovement>().SetGrounded(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision exit with player");
            isPlayerStanding = false;
            // player.GetComponent<PlayerMovement>().SetGrounded(false);
        }
    }
}
