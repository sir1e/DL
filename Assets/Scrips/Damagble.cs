using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagble : MonoBehaviour
{
    public UnityEvent<int, Vector2> daamgbleHit;
    Animator animator;
    [SerializeField]
    private int _max_Health = 100;
    [SerializeField]
    private int _health = 100;
    private bool IsInvincible = false;
    private bool _isAlive = true;
    private float timeSinceHit = 0;
    [SerializeField]
    private float timeSinceHitGlobal= 0f;
    public float invincibilityTime = 0.7f;
    public float healTime = 5f;
   public  int healPerTime = 10;
    public int healPerFunction = 10;
    public int Max_Health
    {
        get
        {
            return _max_Health;
        }
        set
        {
            _max_Health = value;
            
        }

    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value);
            
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timeSinceHitGlobal = timeSinceHitGlobal + Time.deltaTime;
        if (IsInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                IsInvincible = false;
                timeSinceHit = 0f;
            }
            timeSinceHit = timeSinceHit + Time.deltaTime;
        }
  
        if (timeSinceHitGlobal > healTime)
        {
            HealForTime();
        }
    }
    
    public void Hit(int damage, Vector2 knockback)
    {
    
        if (IsAlive && !IsInvincible)
        {
            Health = Health - damage;
            IsInvincible = true;
            animator.SetTrigger("Hit");
            daamgbleHit?.Invoke(damage, knockback);
            CharactersEvents.characterDamaged.Invoke(gameObject, damage);
            timeSinceHitGlobal = 0;




        }
    }

    public void HealForTime()
    {
        if (Health < Max_Health)
        {
            if (Health >= 90)
            {
                healPerFunction = Max_Health - Health;
            }
            Health = Health + healPerTime;
            CharactersEvents.characterHealed.Invoke(gameObject, healPerFunction);

            if (Health > Max_Health)
                Health = 100;
            timeSinceHitGlobal = 0f;

        }
    }
}
