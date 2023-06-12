using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBehindBack : MonoBehaviour
{
    public GameObject player;
    public Skeleton skeleton;
    public float flipCooldown = 1f; // The amount of time in seconds before the skeleton can flip again
    private bool canFlip = true; // Whether the skeleton is allowed to flip

    // ...

    void Update()
    {
        if (GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (canFlip)
            {
                skeleton.FlipDirection();
                canFlip = false;
                Invoke("ResetFlipCooldown", flipCooldown);
            }
        }
    }

    void ResetFlipCooldown()
    {
        canFlip = true;
    }
}

