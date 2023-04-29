using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour
{
    public GameObject projectilePrefab;
    public void FireProjectile()
    {
        Instantiate(projectilePrefab,transform. );
    }

}
