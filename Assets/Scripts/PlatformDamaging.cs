using UnityEngine;

public class PlatformDamaging : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine currentCoroutine;
    private bool hasPlayerTouched = false;
    private Collider2D collider2d;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision enter with player");

            GameSetup gameSetup = collision.gameObject.GetComponentInParent<GameSetup>();
            gameSetup.Hit(1);
            if (gameSetup._health <= 0)
            {
                CheckpointController checkpointController = collision.gameObject.GetComponentInParent<CheckpointController>();
                
                gameSetup.transform.position = new Vector3(checkpointController._startingPosition.x, checkpointController._startingPosition.y, 0);
                gameSetup.IsAlive = true;
                gameSetup.Health = 3;
                gameSetup.heartsContainer[0].SetActive(true);
                gameSetup.heartsContainer[1].SetActive(true);
                gameSetup.heartsContainer[2].SetActive(true);
            }

        }
    }
}
