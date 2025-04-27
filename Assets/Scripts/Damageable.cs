using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibleTime = 2f;
    Animator animator;
    [SerializeField]
    public int _maxHealth = 3;
    [SerializeField]
    private int _health = 3;
    private string param_isAlive = "isAlive";
    public GameObject[] hearts = new GameObject[3];
    

    
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

    public bool ModifyHealth(int healthChange)
    {
        if (_health < MaxHealth)
        {
            _health += healthChange;
            _health = Math.Clamp(_health, 0, MaxHealth);
            return true;
            //zaszla zmiana
        }
        return false;
        //nie zaszla zmiana
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
        if (_health < 1)
        {
            Destroy(hearts[0].gameObject);
        }
        else if (_health < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (_health < 3)
        {
            Destroy(hearts[2].gameObject);
        }
        if (isInvincible)
        {
            if (timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        if (!_isAlive)
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
    
    public bool IsHit {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        } }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            return true;
        }
        return false;
    }
}
