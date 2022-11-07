using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel6Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float shootPlayerInterval;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float bulletSpeed;
    
    private Coroutine _shootingCoroutine;

    [SerializeField] private int minCircleShootingProjectiles;
    [SerializeField] private int maxCircleShootingProjectiles;
    
    [SerializeField] private float minCircleShootingProjectilesRadius;
    [SerializeField] private float maxCircleShootingProjectilesRadius;

    private int NumberOfRandomCircleProjectiles =>
        Random.Range(minCircleShootingProjectiles, maxCircleShootingProjectiles+1);

    public void StartShootingPlayer()
    {
        if (_shootingCoroutine != null) return;
        
        _shootingCoroutine = StartCoroutine(ShootPlayerInternal());
    }

    public void StopShootingPlayer()
    {
        if (_shootingCoroutine == null) return;
        
        StopCoroutine(_shootingCoroutine);
        _shootingCoroutine = null;
    }

    private IEnumerator ShootPlayerInternal()
    {
        while (gameObject.activeSelf)
        {
            if (GameController.singleton.currentActivePlayer == null)
            {
                yield return null;
                continue;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Vector2 playerDir = GameController.singleton.currentActivePlayer.transform.position - transform.position;
            
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * playerDir.normalized;
            
            Debug.Log("bullet velocity = "+ bullet.GetComponent<Rigidbody2D>().velocity);
            
            yield return new WaitForSeconds(shootPlayerInterval);
            
        }
    }

    public void FireInCircle()
    {
        int noOfProjectiles = NumberOfRandomCircleProjectiles;
        
        float angleStep = 360f / noOfProjectiles;

        float angle = 0;

        float randomRadius = Random.Range(minCircleShootingProjectilesRadius, maxCircleShootingProjectilesRadius);
        
        for (int i = 0; i <= noOfProjectiles; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.position * randomRadius;

            GameObject bouncyProjectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            bouncyProjectile.GetComponent<Rigidbody2D>().velocity = dir;

            angle += angleStep;
        }
    }

}
