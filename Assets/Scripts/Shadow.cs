using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Shadow : MonoBehaviour
{
    public enum WalkableDirection
    {
        Right,
        Left
    }

    public float walkSpeed = 5f;
    public DetectionZone attackZone;
    [SerializeField] public WalkableDirection _walkDirection;
    public Vector2 walkDirectionVector = Vector2.right;

    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Vector2 walkDirectionAsVector2;
    Animator animator;

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            _walkDirection = value;

            if (value == WalkableDirection.Left)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.left;
            }
            else if (value == WalkableDirection.Right)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.right;
            }

            touchingDirections.wallCheckDirection = walkDirectionAsVector2;
        }
    }

    public bool _hasTarget;
    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Health Bar UI")]
    [SerializeField] private GameObject healthBarParent;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private float healthBarYOffset = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        UpdateHealthBar();
        if (healthBarParent != null)
            healthBarParent.SetActive(false);
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        UpdateHealthBarPosition();
    }

    private void FixedUpdate()
    {
        touchingDirections.wallCheckDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall) FlipDirection();

        var tempWalkSpeed = walkSpeed;
        if (WalkDirection == WalkableDirection.Left) tempWalkSpeed = -tempWalkSpeed;

        rb.linearVelocity = new Vector2(tempWalkSpeed, rb.linearVelocity.y);
    }

    private void FlipDirection()
    {
        switch (WalkDirection)
        {
            case WalkableDirection.Left:
                WalkDirection = WalkableDirection.Right;
                break;
            case WalkableDirection.Right:
                WalkDirection = WalkableDirection.Left;
                break;
            default:
                break;
        }
        if (healthBarParent != null)
        {
            var scale = healthBarParent.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            healthBarParent.transform.localScale = scale;
            healthBarParent.transform.rotation = Quaternion.identity;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
        if (healthBarParent != null)
        {
            healthBarParent.SetActive(currentHealth < maxHealth && currentHealth > 0);
        }
    }

    private void UpdateHealthBarPosition()
    {
        if (healthBarParent != null)
        {
            healthBarParent.transform.position = transform.position + Vector3.up * healthBarYOffset;
        }
    }

    private void Die()
    {
        if (healthBarParent != null)
            healthBarParent.SetActive(false);
        Destroy(gameObject, 0.5f); 
    }
}
