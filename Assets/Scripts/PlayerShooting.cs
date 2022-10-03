using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    
    [SerializeField] private float fireRate;
    private float nextFire;

    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private Animator animator;

    [field: SerializeField] public float BulletForce { get; private set; }

    [field: SerializeField] public Vector2 TrajDir { get; private set; }


    private void Update()
    {
        AimWhereMouseIs();
        Fire();
    }

    private void Fire()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false && Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            animator.SetTrigger("fire");
            
            GameObject bulletObj = Instantiate(bulletPrefab, shootingPoint.position,firePoint.transform.rotation);

            var bullet = bulletObj.GetComponent<Bullet>();
            
            bullet.rb2D.velocity = firePoint.transform.right * BulletForce;
        }
    }
    
    public void AimWhereMouseIs()
    {
        Vector2 pointA = firePoint.transform.position;
        Vector2 pointB = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        TrajDir = pointB - pointA;
        
        firePoint.transform.right = TrajDir;
    }
}
