    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
using UnityEngine.UI;
    [RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(Damagble))]    
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 11f;
        private bool _IsRunning = false;
        public bool _IsFacingRight = true;
        public float jumpImpulse = 10;
    public Image SkillImage1;
    public Image SkillImage2;
    public float coolDown1 = 3f;
    public float coolDown2 = 5f;
    public bool IsCoolDown1 = false;
    public bool IsCoolDown2 = false;
    TouchingDirections  touchingDirections;
    Damagble damagble;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;
    
        public float Speed
        {
            get
            {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsWalled)
                {
                    if (IsRunning)
                    {
                        return runSpeed;

                    }
                    else return walkSpeed;
                }
                else return 0;
            }
            else return 0;
        }
               
        }
        Vector2 moveInput;
        [SerializeField]
        private bool _isMoving = false;
        public bool IsMoving
        {
            get
            {
                return _isMoving;
            }
            private set
            {
                _isMoving = value;
                animator.SetBool("IsMoving", value);
            }
        }
        [SerializeField]    
 
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                _IsRunning = value;
                animator.SetBool("IsRunning", value);
            }
        }
        public bool IsFacingRight
        {
            get
            {
                return _IsFacingRight;
            }

            private set
            {

                if (_IsFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);

                }
                _IsFacingRight = value;
            }
        }
        Rigidbody2D rb;
        Animator animator;
    public bool lockvelocity
    {
        get
        {
            return animator.GetBool("lockvelocity");
        }
        set{
            animator.SetBool("lockvelocity", value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
            damagble = GetComponent < Damagble > ();
        SkillImage1.fillAmount = 0;
             SkillImage2.fillAmount = 0;
    }
  
        private void FixedUpdate()
        {
        if (isDashing)
        {
            return;
        }
        if(!lockvelocity )
            rb.velocity = new Vector2(moveInput.x * Speed, rb.velocity.y);
            animator.SetFloat("yVelocity", rb.velocity.y);

        if (IsCoolDown1)
        {

            SkillImage1.fillAmount += 1 / coolDown1 * Time.deltaTime;
            if (SkillImage1.fillAmount == 1)
            {
                SkillImage1.fillAmount = 0;
                IsCoolDown1 = false;
            }
        }

        if (IsCoolDown2)
        {
            SkillImage2.fillAmount += 1 / coolDown2 * Time.deltaTime;
            if (SkillImage2.fillAmount == 1)
            {
                SkillImage2.fillAmount = 0;
                IsCoolDown2 = false;
            }
        }   
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (damagble.timeSinceHitGlobal > damagble.healTime)
        {
            damagble.HealForTime();
        }
    }
    public  void OnMove(InputAction.CallbackContext context)
        {
      if (damagble.IsAlive)
        {
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
            SetDirection(moveInput);
        }
        else IsMoving = false;
       
            
        }

        private void SetDirection(Vector2 moveInput)
        {
            if(moveInput.x >0 && !IsFacingRight)
            {
                IsFacingRight = true;

            } else if (moveInput.x < 0 &&  IsFacingRight)
            {
                IsFacingRight = false;
            }
        }
        public void OnRun(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                IsRunning = true;
            
            }
            else if (context.canceled)
            {
                IsRunning = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        //check if alive
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnSuperAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!IsCoolDown2)
            {
                if (damagble.Mana >= 50)
                {
                    IsCoolDown2 = true;
                    animator.SetTrigger("super_attack");
                    damagble.Mana -= 50;
                }
            }
       
           
        }
        
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!IsCoolDown1)
            {
                if (damagble.Mana >= 30)
                {
                    IsCoolDown1 = true;
                    animator.SetTrigger("ranged_attack");
                    damagble.Mana -= 30;

                }
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        
        lockvelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private IEnumerator Dash()
    {
    
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 dashDirection = IsFacingRight ? Vector2.right : Vector2.left;
        rb.velocity = dashDirection * dashingPower;
        damagble.IsInvincible = true;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        damagble.IsInvincible = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    }
    