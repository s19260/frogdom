using UnityEngine;

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

    public bool _hasTarget;

    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Vector2 walkDirectionAsVector2;

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            // Make changes only if new
            _walkDirection = value;

            if (value == WalkableDirection.Left)
            {
                // Facing left so negative scale
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.left;
            }
            else if (value == WalkableDirection.Right)
            {
                // Facing right so positive scale
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                walkDirectionAsVector2 = Vector2.right;
            }

            touchingDirections.wallCheckDirection = walkDirectionAsVector2;
        }
    }

    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
           animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
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
                Debug.Log(" skret w prawo");
                break;
            case WalkableDirection.Right:
                WalkDirection = WalkableDirection.Left;
                Debug.Log(" skret w lewo");
                break;
            default:
                Debug.Log(name + "'s WalkDirection is not set to Left or Right");
                break;
        }
    }
}