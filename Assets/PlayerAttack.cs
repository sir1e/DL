using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    Damagble playerDamagble;
    


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
        Damagble damagble = collision.GetComponent<Damagble>();
        if (damagble != null)
        {
            if(damagble.IsAlive)
            {
                damagble.Hit(attackDamage, knockback);
                playerDamagble.timeSinceHitGlobal = 0;
            }
        }
    }
}