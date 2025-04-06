using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D touchingFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    public Vector2 wallCheckDirection = Vector2.zero;
    

    CapsuleCollider2D col;
    Animator animator;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;


    public bool IsGrounded { get{
         return _isGrounded;
    } private set{
        _isGrounded = value;
        animator.SetBool(AnimationStrings.isGrounded, value);
    } }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall { get{
         return _isOnWall;
    } private set{
        _isOnWall = value;
        animator.SetBool(AnimationStrings.isOnWall, value);
    } }


    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = col.Cast(Vector2.down, touchingFilter, groundHits,  groundDistance) > 0;
        IsOnWall = col.Cast(wallCheckDirection, touchingFilter, wallHits, wallDistance) > 0;
    }
    
}
