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

            Damageable damageable = collision.gameObject.GetComponentInParent<Damageable>();

            damageable.ModifyHealth(-3);
            if (damageable._health <= 0)
            {
                CheckpointController checkpointController = collision.gameObject.GetComponentInParent<CheckpointController>();
                
                damageable.transform.position = new Vector3(checkpointController._startingPosition.x, checkpointController._startingPosition.y, 0);
                damageable.IsAlive = true;
                damageable.Health = 3;
                damageable.heartsContainer[0].SetActive(true);
                damageable.heartsContainer[1].SetActive(true);
                damageable.heartsContainer[2].SetActive(true);
            }

        }
    }

}
