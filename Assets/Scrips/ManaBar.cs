using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    Damagble playerDamagble;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamagble = player.GetComponent<Damagble>();
    }


    // Update is called once per frame
    void Update()
    {
        manaBar.value = playerDamagble.Mana;
    }
}
