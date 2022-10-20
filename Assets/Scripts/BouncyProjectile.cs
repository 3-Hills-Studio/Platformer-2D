using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncyProjectile : MonoBehaviour
{

    [SerializeField] private GameObject firePrefab;

    [SerializeField] private Transform firePoint;
    
    [SerializeField] private float radius;
    

    [SerializeField] private float randomStartTime;
    [SerializeField] private float randomEndTime;
    
    
    [SerializeField] private int randomStartValueNumberOfProjectiles;
    [SerializeField] private int randomEndValueNumberOfProjectiles;

    [SerializeField] private float damage;
    
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(randomStartTime, randomEndTime));

        SpawnFire();
        
        Destroy(gameObject);
    }

    private void SpawnFire()
    {
        int numberOfProjectiles =
            Random.Range(randomStartValueNumberOfProjectiles, randomEndValueNumberOfProjectiles + 1);
        
        float angleStep = 360f / numberOfProjectiles;

        float angle = 0;

        for (int i = 0; i <= numberOfProjectiles; i++)
        {

            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.position * radius;

            GameObject fire = Instantiate(firePrefab, firePoint.position, Quaternion.identity);

            fire.GetComponent<Rigidbody2D>().velocity = dir;

            angle += angleStep;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Health health))
        {
            health.ModifyHealth(-damage);
            
            SpawnFire();

            Destroy(gameObject);
            return;
        }
    }
}
