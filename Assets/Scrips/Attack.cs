using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 15;
    private Vector2 knockback = new Vector2(0,0);

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
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
        if(damagble != null)
        {
            Vector2 vectroknockaback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damagble.Hit(attackDamage, vectroknockaback);
            damagble.timeSinceHitGlobal = 0;
        }
    }
}
