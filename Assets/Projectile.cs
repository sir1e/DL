using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 prospeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    GameObject player;
    Damagble playerDamagble;

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
            damagble.Hit(damage, vectroknockaback);
            playerDamagble.timeSinceHitGlobal = 0;
            Destroy(gameObject);
        }
    }
}
