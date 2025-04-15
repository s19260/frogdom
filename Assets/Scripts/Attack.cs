using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;

    public int attackDamage = 40;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.Hit(attackDamage);
        }
    }
}
