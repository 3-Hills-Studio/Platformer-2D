using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel3Shooting : MonoBehaviour
{

    [SerializeField] private int numberOfProjectiles;

    [SerializeField] private float bouncyProjectileForce;
    
    [SerializeField] private GameObject bouncyProjectilePrefab;

    [SerializeField] private float waitTimePerBouncyProjectile;
    
    [SerializeField] private float waitTimeWhenFullCircleIsOver;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float radius;
    

    private void Start()
    {
        StartCoroutine(FireBouncyProjectiles());
    }

    private IEnumerator FireBouncyProjectiles()
    {

        while (gameObject.activeSelf)
        {
            float angleStep = 360f / numberOfProjectiles;

            float angle = 0;

            for (int i = 0; i <= numberOfProjectiles; i++)
            {

                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.position * radius;

                GameObject bouncyProjectile = Instantiate(bouncyProjectilePrefab, firePoint.position, Quaternion.identity);

                bouncyProjectile.GetComponent<Rigidbody2D>().velocity = dir;

                angle += angleStep;

                yield return new WaitForSeconds(waitTimePerBouncyProjectile);
            }

            yield return new WaitForSeconds(waitTimeWhenFullCircleIsOver);
        }
    }


}
