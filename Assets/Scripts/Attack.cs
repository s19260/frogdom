using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;

    public Vector2 knockback = Vector2.zero;
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
//                Debug.Log(collision.name + " hit for " + attackDamage);
                if (shadow != null)
                {
                    shadow.TakeDamage(attackDamage);
                }
            }
        }
    }

}
