using UnityEngine;

public class PlatformBounce : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    Animator animator;
    private void Start()
    {
        playerRigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision enter with player");
       //     animator.SetTrigger(AnimationStrings.isBounce);
            playerRigidBody.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
    }
}
