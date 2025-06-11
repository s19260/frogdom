using UnityEngine;

public class JumpPowerUpScript : MonoBehaviour
{
    public bool hasJumpPowerUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
            PlayerController playerController = other.GetComponent<PlayerController>();

            collidingGameSetup.AddJumpPowerUp();
            if (collidingGameSetup._jumpPowerUp)
            {
                Debug.Log("Jump Power Up  1");
                collidingGameSetup.jumpPowerUpContainer[0].SetActive(true);
                hasJumpPowerUp = true;
                Debug.Log(hasJumpPowerUp);
                playerController.jumpInpulse = playerController.jumpInpulse * 1.3f;

            }
            Destroy(this.gameObject);
        }
    }
}