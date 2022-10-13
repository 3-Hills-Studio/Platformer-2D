using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private float projectileForce;
    
    public void Fire()
    {
        var projectile = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.rotation);

        projectile.GetComponent<EnemyBullet>().rb2D.velocity = firePoint.right * projectileForce;
    }

}
