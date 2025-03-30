using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))] 
    public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 40f;
    public float runSpeed = 8f;
    public float jumpInpulse = 60f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

public float CurrentMoveSpeed { get
    {
        if(IsMoving)
        {   
            if(IsRunning)
            {
                return runSpeed;
            } else
        {
            return walkSpeed;
        }
    } 
    else
    {
        return 0;
    }
    } }


    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { 
        get {
        return _isMoving; 
    } private set {
        _isMoving = value;
        animator.SetBool(AnimationStrings.isMoving, value);
    } 
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning {   
        get {
            return _isRunning;
    }
    private set {
        _isRunning = value;
        animator.SetBool(AnimationStrings.isRunning, value);
    }
    }
    Rigidbody2D rb;
    Animator animator;
    public bool _isFacingRight = true;
    public bool IsFacingRight {get { return _isFacingRight; } private set {
        if(_isFacingRight != value)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;
    } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {        
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    public void SetFacingDirection(Vector2 moveInput){
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        } else if(context.canceled)
        {      
            IsRunning = false;
        }  
    }
    public void OnJump(InputAction.CallbackContext context){
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpInpulse);
        }
    }
}
