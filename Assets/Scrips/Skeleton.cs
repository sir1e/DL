using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Skeleton : MonoBehaviour
{
    public GameObject player;
    public float walk_speed = 5f;
   // public float walkStopRate = 0.05f;
    TouchingDirections TouchingDirections;
    Rigidbody2D rb;
    public DetectionZone attackZone;
    Animator animator;

    public enum WalkDirection1
    { 
        Right, Left
    }
    private WalkDirection1 _walkDirection;
    private Vector2 WalkDirectionVector = Vector2.right;
    public WalkDirection1 WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                // Update the localScale to flip the object horizontally
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;

                // Update the WalkDirectionVector based on the new direction
                WalkDirectionVector = (value == WalkDirection1.Right) ? Vector2.right : Vector2.left;
            }

            _walkDirection = value; // Update the _walkDirection variable last
        }
    }


   

    public void FlipDirection()
    {
        if (WalkDirection == WalkDirection1.Right)
        {
            WalkDirection = WalkDirection1.Left;
        }
        else if (WalkDirection == WalkDirection1.Left)
        {
            WalkDirection = WalkDirection1.Right;
        }
        else
        {
            Debug.LogError("Неправильні дані повороту");
        }

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TouchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }
    public bool lockvelocity
    {
        get
        {
            return animator.GetBool("lockvelocity");
        }
        set
        {
            animator.SetBool("lockvelocity", value);
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("CanMove");
        }

    }

    public float AttackCoolDown
    {
        get
        {
            return animator.GetFloat("attackCooldown");
        }
        private set
        {
            animator.SetFloat("attackCooldown", value);
        }
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCoolDown >0)   
        AttackCoolDown -= Time.deltaTime;
        }
    private void FixedUpdate()
    {
        if (TouchingDirections.IsWalled && TouchingDirections.IsGrounded)
        {
            FlipDirection();
        }
        if (CanMove)
            rb.velocity = new Vector2(walk_speed * WalkDirectionVector.x, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        
        lockvelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }


}
