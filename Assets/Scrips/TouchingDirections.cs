using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    CapsuleCollider2D touchingCol;
    RaycastHit2D [] groundHits = new RaycastHit2D [5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    Animator animator;
    private Vector2 wallcheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    [SerializeField]
    private bool _isGrounded;

    

    public bool IsGrounded

    {
        get { return _isGrounded; }
        private set {
            _isGrounded = value;
            animator.SetBool("IsGrounded", value);
        }
    }

    [SerializeField]
    private bool _isWalled;
    public bool IsWalled
    {
        get { return _isWalled; }
        private set
        {
            _isWalled = value;
            animator.SetBool("IsWalled", value);
        }
    }

    private void Awake()
        {
            touchingCol = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
       

    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsWalled = touchingCol.Cast(wallcheckDirection, castFilter, wallHits, wallDistance) > 0;
        
    }
}
