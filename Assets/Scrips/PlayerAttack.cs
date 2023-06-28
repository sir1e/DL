using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlayerAttack : MonoBehaviour
{
    protected Collider2D attackCollider;

   [SerializeField] public int attackDamage;
    public Vector2 knockback = Vector2.zero;

    protected Damagble playerDamagble;
    


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        attackCollider = GetComponent<Collider2D>();
        playerDamagble = player.GetComponent<Damagble>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision, knockback);
        playerDamagble.timeSinceHitGlobal = 0;
    }

   public void Attack(Collider2D collision, Vector2 knockback)
    {
        Damagble damagble = collision.GetComponent<Damagble>();
        if (damagble != null)
        {
            if (damagble.IsAlive)
            {
                damagble.Hit(attackDamage, knockback);
                DateTime CurrentDate = DateTime.Now;
                string objectName = gameObject.transform.parent != null ? gameObject.transform.parent.name : "Unknown";
                string message = $"„ас {CurrentDate} ќб'Їкт {objectName} ударив з силою {attackDamage} шкоди\n";

                string path = @"Assets\Logs.txt";
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
    
}