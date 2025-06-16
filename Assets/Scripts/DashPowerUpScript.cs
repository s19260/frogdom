using UnityEngine;

public class DashPowerUpScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
            collidingGameSetup.AddDashPowerUp();
            if (collidingGameSetup._dashPowerUp)
            {
                Debug.Log("Dash Up ");
                collidingGameSetup.dashPowerUpContainer[0].SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
