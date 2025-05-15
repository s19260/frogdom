using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthBonus = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Health Pickup");
            GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
           
            
                if (collidingGameSetup && collidingGameSetup.ModifyHealth(healthBonus))
                {
                    Debug.Log(collidingGameSetup.heartsContainer[collidingGameSetup._health-1]);
                    collidingGameSetup.heartsContainer[collidingGameSetup._health-1].SetActive(true);


                    Destroy(this.gameObject);
                }
            
        }
        

    }
}
