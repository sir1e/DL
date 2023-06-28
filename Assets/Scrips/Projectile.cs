using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PlayerAttack
{

    public Vector2 prospeed = new Vector2(3f, 0);
    GameObject player;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDamagble = player.GetComponent<Damagble>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(prospeed.x * transform.localScale.x, prospeed.y);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Damagble damagble = collision.GetComponent<Damagble>();

        if(damagble != null)
        {
            Vector2 vectroknockaback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            Attack(collision, vectroknockaback);
            playerDamagble.timeSinceHitGlobal = 0;
            Destroy(gameObject);
        }
    }
}
