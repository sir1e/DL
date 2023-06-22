using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

public class Damagble : MonoBehaviour
{
    public UnityEvent<int, Vector2> daamgbleHit;
    PlayerController playerController;
    Animator animator;
    [SerializeField]
    private int _max_Health = 100;
    private int _max_Mana = 100;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _mana = 100;
    public  bool IsInvincible = false;
    private bool _isAlive = true;
    private float timeSinceHit = 0;
    [SerializeField]
    public float timeSinceHitGlobal= 0f;
    public float invincibilityTime = 0.7f;
    public float healTime = 7f;
   private  int healPerTime = 10;
    private int healPerFunction = 10;
    public bool _InBattle = false;
    private bool isDashingCoolDown = false;
    Damagble damagble;
    GameObject player;
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
    public int Max_Mana
    {
        get
        {
            return _max_Mana;
        }
        set
        {
            _max_Mana = value;

        }

    }
    private void AddingMana(int manaTick)
    {
        damagble.Mana += manaTick;
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
                InBattle = false;
                if(Mana == Max_Mana) AddingMana(10);
                else AddingMana(10);

            }
        }
    }
    public int Mana
    {
        get
        {
            return _mana;
        }
        set
        {
                        _mana = value;
          
        }
    }

    public bool InBattle
    {
        get
        {
            return _InBattle;
        }
        set
        {
            _InBattle = value;
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
        playerController = GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        damagble = player.GetComponent<Damagble>();
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
  
     
    }
    
    public void Hit(int damage, Vector2 knockback)
    {
    
        if (IsAlive && !IsInvincible)
        {
            string object1 = gameObject.name;
            DateTime CurrentDate = DateTime.Now;
            InBattle = true;
            Health = Health - damage;
            IsInvincible = true;
            animator.SetTrigger("Hit");
            daamgbleHit?.Invoke(damage, knockback);
            CharactersEvents.characterDamaged.Invoke(gameObject, damage);
            timeSinceHitGlobal = 0;
            if(gameObject == player)
            StartCoroutine(DashCooldownCoroutine());
            string message = "„ас "+ CurrentDate + " ќб'Їкт "+ object1 + " отримав " + damage + " шкоди";

            string path = "Logs.txt";
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(message);
            writer.Close();
        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        if (!isDashingCoolDown)
        {
            isDashingCoolDown = true;
            playerController.canDash = false;

            yield return new WaitForSeconds(playerController.dashingCoolDown);

            playerController.canDash = true;
            isDashingCoolDown = false;
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
