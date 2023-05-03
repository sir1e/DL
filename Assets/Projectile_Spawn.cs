using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour
{
    public Transform startPoint;
    public GameObject projectilePrefab;
    public void FireProjectile()
    {
        Instantiate(projectilePrefab, startPoint.position, projectilePrefab.transform.rotation);
    }

}
