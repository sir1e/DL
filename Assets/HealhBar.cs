using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhBar : MonoBehaviour
{
    public Slider healthBar;
    Damagble playerDamagble;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamagble = player.GetComponent<Damagble>();
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerDamagble.Health;
    }
}
