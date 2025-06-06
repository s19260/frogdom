using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 40f;
    public float runSpeed = 8f;
    public float dashSpeed = 100f;
    public float jumpInpulse = 60f;


    [SerializeField] 
    private bool _isMoving;

    [SerializeField] 
    private bool _isRunning;

    [SerializeField]
    private bool _isDashing;

    public bool _isFacingRight = true;
    private Animator animator;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    
    public float dashCooldown = 1f;  // Adjust in Inspector
    private bool canDash = true;
    public Slider dashCooldownSlider;
    
    // Direction on movement is determined by input value
    
    void Start()
    {
        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = dashCooldownSlider.maxValue;
        }
    }
    
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                if (IsRunning) return runSpeed;

                return walkSpeed;
            }

            return 0;
        }
    }

    // As Dash can be executed even on idle state direction is determined by sprite facing direction
    public float CurrentDashSpeed
    {
        get
        {
            if (IsDashing)
            {
                // Important! We assume we only dash in X right now
                if (IsFacingRight)
                    return dashSpeed;
                else
                    return -dashSpeed;
            }

            return 0;
        }
    }

    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool IsDashing
    {
        get => _isDashing;
        private set
        {
            _isDashing = value;
            animator.SetBool(AnimationStrings.isDashing, value);
        }
    }

    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value) transform.localScale *= new Vector2(-1, 1);
            _isFacingRight = value;
        }
    }
    
    private void Awake()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed + CurrentDashSpeed, rb.linearVelocity.y);
       // Debug.Log(rb.linearVelocity);
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
            IsFacingRight = true;
        else if (moveInput.x < 0 && IsFacingRight) IsFacingRight = false;
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;
        else if (context.canceled) IsRunning = false;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpInpulse);
        }
    }
    private bool dashButtonHeld = false;

    public void OnDash(InputAction.CallbackContext context)
    {
        GameSetup gameSetup = gameObject.GetComponent<GameSetup>();

        if (context.performed && canDash && gameSetup._dashPowerUp && !dashButtonHeld)
        {
            dashButtonHeld = true;
            animator.SetTrigger("dashAnimation"); // Trigger dash animation
            StartCoroutine(PerformDash());
        }
        else if (context.canceled)
        {
            dashButtonHeld = false;
            IsDashing = false;
        }
    }


    private IEnumerator PerformDash()
    {
        canDash = false;
        IsDashing = true;
    
        rb.linearVelocity = new Vector2(20f, rb.linearVelocity.y);
    
        animator.SetTrigger(AnimationStrings.dash);

        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = 0f;
        }
        
        float elapsed = 0f;
        while (elapsed < dashCooldown)
        {
            elapsed += Time.deltaTime;
            if (dashCooldownSlider != null)
            {
                dashCooldownSlider.value = Mathf.Clamp01(elapsed / dashCooldown) * dashCooldownSlider.maxValue;
            }
            yield return null;
        }

        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = dashCooldownSlider.maxValue;
        }
    
        IsDashing = false;
        canDash = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        GameSetup gameSetup = gameObject.GetComponent<GameSetup>();
        if (!gameSetup)
        {
            Debug.LogError("GameSetup nie zostal wykryty.");
            return;
        }

        if (gameSetup._attackPowerUp)
        {
            animator.SetBool(AnimationStrings.attack2, true);
        }
        
        if (context.started) animator.SetTrigger(AnimationStrings.attack);
    }

    public void onHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}