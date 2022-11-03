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

    public void StartShootingPlayer()
    {
        if (_shootingCoroutine != null) return;
        
        _shootingCoroutine = StartCoroutine(ShootPlayerInternal());
    }

    public void StopShootingPlayer()
    {
        if (_shootingCoroutine == null) return;
        
        StopCoroutine(_shootingCoroutine);
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
            
            yield return new WaitForSeconds(shootPlayerInterval);
            
        }
    }


}
