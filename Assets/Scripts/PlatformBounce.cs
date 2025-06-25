using UnityEngine;

public class PlatformBounce : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator animator;

    private void Start()
    {
        playerRigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("isBounce"); 
            playerRigidBody.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
    }
}