using UnityEngine;

public class AttackPowerUpScript : MonoBehaviour
{
    
    public bool hasAttackPowerUp = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
            
            collidingGameSetup.AddAttackPowerUp();
            if (collidingGameSetup._attackPowerUp)
            {
                Debug.Log("Attack Up ");
                
                collidingGameSetup.attackPowerUpContainer[0].SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
