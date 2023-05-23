using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour
{
    public Transform startPoint;
    public GameObject projectilePrefab;
    public float projectileLifetime = 3f;       
    public void FireProjectile()
    {
 
            GameObject projectile = Instantiate(projectilePrefab, startPoint.position, projectilePrefab.transform.rotation);

            Vector3 origScale = projectile.transform.localScale;
            float direction = Mathf.Sign(startPoint.localScale.x);

            projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1 , origScale.y, origScale.z);

        Destroy(projectile, projectileLifetime);
    }

}
