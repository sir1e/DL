    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    [RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(Damagble))]    
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 11f;
        private bool _IsRunning = false;
        public bool _IsFacingRight = true;
        public float jumpImpulse = 10;
        TouchingDirections  touchingDirections;
    Damagble damagle;
    
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
        damagle = GetComponent < Damagble > ();
        }
  
        private void FixedUpdate()
        {
        if(!lockvelocity )
            rb.velocity = new Vector2(moveInput.x * Speed, rb.velocity.y);
            animator.SetFloat("yVelocity", rb.velocity.y);
        }
      public  void OnMove(InputAction.CallbackContext context)
        {
      if (damagle.IsAlive)
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
            if(damagle.Mana >= 50)
            {
                animator.SetTrigger("super_attack");
                damagle.Mana -= 50;
            }
           
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (damagle.Mana >= 30)
            {
                animator.SetTrigger("ranged_attack");
                damagle.Mana -= 30;
            }

        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        
        lockvelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    }
    