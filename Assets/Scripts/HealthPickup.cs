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
            Damageable collidingDamageable = other.GetComponentInParent<Damageable>();
           
            
                if (collidingDamageable && collidingDamageable.ModifyHealth(healthBonus))
                {
                    Destroy(this.gameObject);
                }
            
        }
        

    }
}
