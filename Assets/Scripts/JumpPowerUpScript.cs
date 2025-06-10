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

                // Show the UI and freeze the game
                PowerUpUIManager uiManager = FindObjectOfType<PowerUpUIManager>();
                if (uiManager != null)
                {
                    uiManager.Show("You got Jump Power Up!\nPress Space\n or Enter to continue.");
                }
            }
            Destroy(this.gameObject);
        }
    }
}