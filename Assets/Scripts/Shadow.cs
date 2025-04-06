using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Shadow : MonoBehaviour
{

    public float walkSpeed = 5f;

    Rigidbody2D  rb;
    TouchingDirections touchingDirections;
        public DetectionZone clifEdgeZone;


    public enum WalkableDirection { Right, Left};
    [SerializeField]
    public WalkableDirection _walkDirection;
    public Vector2 walkDirectionVector = Vector2.right;
    Vector2 walkDirectionAsVector2;

    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
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
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
                touchingDirections.wallCheckDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        rb.linearVelocity = new Vector2(walkSpeed, rb.linearVelocity.y);
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right){
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left){
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Currernt walkable direction is not set to legal value");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
