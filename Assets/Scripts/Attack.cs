using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;

    public int attackDamage = 50;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameSetup gameSetup = collision.GetComponent<GameSetup>();
        Shadow shadow = collision.GetComponent<Shadow>();

        if (gameSetup != null)
        {
            bool gotHit = gameSetup.Hit(attackDamage);
            if (gotHit)
            {
                if (shadow != null)
                {
                    shadow.TakeDamage(attackDamage);
                }
            }
        }
    }
}
