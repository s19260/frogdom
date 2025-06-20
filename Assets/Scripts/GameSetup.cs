using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameSetup : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    [SerializeField]
    private bool _isAlive = true;
    private bool _hasAttackPowerUp = false;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibleTime = 2f;
    Animator animator;
    [SerializeField]
    public int _maxHealth = 3;
    [SerializeField]
    public int _health = 3;
    [SerializeField]
    public int _keys = 0;
    public bool _jumpPowerUp = false;
    public bool _attackPowerUp = false;
    public bool _dashPowerUp = false;
    public int deathCounter = 0;
    public int _trash = 0;
    
    private string param_isAlive = "isAlive";
    [FormerlySerializedAs("hearts")] public GameObject[] heartsContainer = new GameObject[3];
    [FormerlySerializedAs("keys")] public GameObject[] keysContainer = new GameObject[3];
    [SerializeField]
    public GameObject[] jumpPowerUpContainer = new GameObject[1];
    public GameObject[] attackPowerUpContainer = new GameObject[1];
    public GameObject[] dashPowerUpContainer = new GameObject[1];
    [SerializeField] private DamageFlash damageFlash; // assign in Inspector

    

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

    public bool Heal(int addedHealth)
    {
        if (_health <= MaxHealth)
        {
            _health += addedHealth;
            _health = Math.Clamp(_health, 0, MaxHealth);
            
            return true;
            //zaszla zmiana
        }
    return false;
    //nie zaszla zmiana
    }

    public void AddKey()
    {
        _keys++;
    }

    public void AddJumpPowerUp()
    {
        _jumpPowerUp = true;
    }

    public void AddAttackPowerUp()
    {
        _attackPowerUp = true;
    }

    public void AddDashPowerUp()
    {
        _dashPowerUp = true;
    }

    public void AddTrash()
    {
        _trash++;
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
            animator.SetBool(param_isAlive, _isAlive);
        }
    }
    Rigidbody2D rb;
    

    void Start()
    {
        if(CompareTag("Player")){
            keysContainer[0].SetActive(false);
            keysContainer[1].SetActive(false);
            keysContainer[2].SetActive(false);
            jumpPowerUpContainer[0].SetActive(false);
            attackPowerUpContainer[0].SetActive(false);
            dashPowerUpContainer[0].SetActive(false);
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool(param_isAlive, IsAlive);
    }

    private void Update()
    {
       if(CompareTag("Player"))
        if (_health < 1)
        {
            heartsContainer[0].SetActive(false);
        }
        else if (_health == 1)
        {
            heartsContainer[1].SetActive(false);
        }
        else if (_health == 2)
        {
            heartsContainer[2].SetActive(false);
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
    }

    public void KillPlayer()
    {
        IsAlive = false;
        deathCounter++;
        GetComponent<PlayerInputSender>().SendDeathCount(deathCounter);
        
        IsAlive = true;
        Health = 3;
        heartsContainer[0].SetActive(true);
        heartsContainer[1].SetActive(true);
        heartsContainer[2].SetActive(true);
        //Teleporting to last checkpoint
        CheckpointController checkpointController = gameObject.GetComponent<CheckpointController>();
        if (checkpointController)
        {
            transform.position = new Vector3(checkpointController._startingPosition.x,
                checkpointController._startingPosition.y, 0);
        }    }

    public bool HasAttackPowerUp
    {
        get
        {
            return _attackPowerUp;
        }
        set
        {
            _hasAttackPowerUp = value;
            animator.SetBool(AnimationStrings.attack2, value);
        }
    }
    public bool IsHit {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        set
        {
            animator.SetTrigger(AnimationStrings.isHit);
            if (damageFlash != null)
                damageFlash.Flash();
        } 
    }

    
    public bool Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            IsHit = true;
            damageableHit?.Invoke(damage, Vector2.zero);
            if (Health <= 0 && gameObject.CompareTag("Player"))
            {
                {
                    KillPlayer();
                }
            }
            return true;
        }
        return false;
    }
}
