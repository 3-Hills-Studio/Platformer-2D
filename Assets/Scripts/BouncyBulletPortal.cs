using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncyBulletPortal : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject bouncyBulletPrefab;

    [SerializeField] private float bulletForce;

    [SerializeField] private int startNumberOfBullets;
    
    [SerializeField] private int endNumberOfBullets;

    [SerializeField] private float spawnDelay;

    [SerializeField] private Vector3 rotation;
    
    [SerializeField] private float rotationDuration;
    
    private void Start()
    {
        RotateFirePoint();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnBullets());
    }

    public IEnumerator SpawnBullets()
    {
        int numberOfBullets = Random.Range(startNumberOfBullets, endNumberOfBullets);

        for (int i = 0; i < numberOfBullets; i++)
        {
            var bullet = Instantiate(bouncyBulletPrefab, firePoint.position, firePoint.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletForce;

            yield return new WaitForSeconds(spawnDelay);
        }
        
        gameObject.SetActive(false);
    }

    private void RotateFirePoint()
    {
        firePoint.DORotate(rotation, rotationDuration).SetLoops(-1,LoopType.Yoyo);
    }

}
