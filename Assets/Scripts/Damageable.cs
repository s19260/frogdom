using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibleTime = 0.25f;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _health = 100;
    private string param_isAlive = "isAlive";

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
            if (_health <= 0)
                IsAlive = false;
        }
    }
    

    public int Health {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
            
        }
    }


    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            //animator.SetBool(AnimationStrings.isAlive, value);
            animator.SetBool(param_isAlive, _isAlive);
            Debug.Log("Is Alive set " + value);
        }
    }
    Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool(param_isAlive, IsAlive);
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        Hit(25);
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
