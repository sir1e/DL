using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Attack : PlayerAttack
{
    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 vectorKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        Attack(collision, vectorKnockback);
    }
}
