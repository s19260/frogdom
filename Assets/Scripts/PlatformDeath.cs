using UnityEngine;

public class PlatformDeath : MonoBehaviour
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
            if (gameSetup)
            {
                gameSetup.KillPlayer();
            }
        }
    }

}
