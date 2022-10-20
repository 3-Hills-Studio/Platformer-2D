using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossLevel3Shooting : MonoBehaviour
{

    [SerializeField] private int numberOfProjectiles;

    [SerializeField] private float bouncyProjectileForce;
    
    [SerializeField] private GameObject bouncyProjectilePrefab;

    [SerializeField] private float waitTimePerBouncyProjectile;
    
    [SerializeField] private float waitTimeWhenFullCircleIsOver;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float radiusStart;
    
    [SerializeField] private float radiusEnd;

    [SerializeField] private Health health;

    [SerializeField] private int numberOfProjectilesPhase1;
    
    [SerializeField] private int numberOfProjectilesPhase2;
    
    private float healthDivision = 3;

    [SerializeField] private List<GameObject> bouncyBulletPortals;

    [SerializeField] private float enablePortalsEveryInSeconds;

    private Coroutine _openingPortalsCoroutine;

    public bool stopShooting = false;
    
    private void Start()
    {
        StartCoroutine(FireBouncyProjectiles());
    }

    public void StopShooting()
    {
        stopShooting = true;
    }

    private void Update()
    {
        if (stopShooting) return;
        
        if (health.CurrentHealth <= (health.MaxHealth/healthDivision) * 2 && health.CurrentHealth > (health.MaxHealth/healthDivision))
        {
            numberOfProjectiles = numberOfProjectilesPhase1;
            Debug.Log("phase 1: "+health.CurrentHealth);
            return;
        }
        if (health.CurrentHealth <= (health.MaxHealth/healthDivision))
        {
            numberOfProjectiles = numberOfProjectilesPhase2;

            if (_openingPortalsCoroutine == null)
            {
                _openingPortalsCoroutine = StartCoroutine(OpenPortals());
            }

            Debug.Log("phase 2: "+health.CurrentHealth);
            return;
        }
    }

    private IEnumerator FireBouncyProjectiles()
    {
        while (stopShooting == false)
        {
            
            Debug.Log("stop shooting "+stopShooting);
            
            float angleStep = 360f / numberOfProjectiles;

            float angle = 0;

            for (int i = 0; i <= numberOfProjectiles; i++)
            {

                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.position * Random.Range(radiusStart,radiusEnd);

                GameObject bouncyProjectile = Instantiate(bouncyProjectilePrefab, firePoint.position, Quaternion.identity);

                bouncyProjectile.GetComponent<Rigidbody2D>().velocity = dir;

                angle += angleStep;

                if (stopShooting == true) yield break;
                
                yield return new WaitForSeconds(waitTimePerBouncyProjectile);
            }

            yield return new WaitForSeconds(waitTimeWhenFullCircleIsOver);
        }
    }

    private IEnumerator OpenPortals()
    {
        while (!stopShooting)
        {
            int randomIndex = Random.Range(0, bouncyBulletPortals.Count);
            
            bouncyBulletPortals[randomIndex].SetActive(true);

            yield return new WaitForSeconds(enablePortalsEveryInSeconds);

        }
    }


}
