using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;

    public Vector2 knockback = Vector2.zero;
    public int attackDamage = 50;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(attackDamage, knockback);
            if (gotHit){
            Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }
    }
}
