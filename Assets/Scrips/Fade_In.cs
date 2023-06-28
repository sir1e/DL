using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_In : MonoBehaviour
{
    Animator animator;

    Damagble playerDamagble;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamagble = player.GetComponent<Damagble>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDamagble.IsAlive )
        {
            animator.SetTrigger("Fade_In");
        }
    
    }
}
