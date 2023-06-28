using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Attack : MonoBehaviour
{   
    Collider2D attackCollider;
    public int attackDamage = 15;
    public Vector2 knockback = Vector2.zero;

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
            DateTime CurrentDate = DateTime.Now;
            string object1 = gameObject.transform.parent.name;
            string message = "„ас " + CurrentDate + " ќб'Їкт "+ object1 + " ударив з силою " + attackDamage + " шкоди\n";
            string path = @"Assets\Logs.txt";
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
