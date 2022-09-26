using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPatrol : Patrol
{

    public Coroutine patrolCoroutine;

    public LayerMask playerLayerMask;

    public float detectionRadius;

    public GameObject bulletPrefab;

    public float bulletForce = 2;

    public float shootAfterInSeconds = 1;
    
    public override void Start()
    {
        transform.position = patrolPoints[0].position;
        StartPatrol();
        StartCoroutine(ScanForPlayer());
    }

    public override void StartPatrol()
    {
        patrolCoroutine = StartCoroutine(PatrolLoop());
        Debug.Log("Patrol started!");
    }
    
    [ContextMenu("StopPatrol")]
    public void StopPatrolCoroutine()
    {
        Debug.Log("Patrol stopped!");
        StopCoroutine(patrolCoroutine);
        patrolCoroutine = null;
    }


    public IEnumerator ScanForPlayer()
    {

        while (gameObject.activeSelf)
        {

            var circleCastObjects =
                Physics2D.CircleCastAll(transform.position, detectionRadius, Vector2.zero, 0, playerLayerMask);

            if (circleCastObjects.Length > 0)
            {
                if(patrolCoroutine != null) StopPatrolCoroutine();
                
                for (int i = 0; i < circleCastObjects.Length; i++)
                {
                    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                    Vector2 dir = circleCastObjects[i].transform.position - transform.position;

                    bullet.GetComponent<EnemyBullet>().rb2D.velocity = dir * bulletForce;

                }
                
                yield return new WaitForSeconds(shootAfterInSeconds);
                
                continue;
            }

            if(patrolCoroutine == null) StartPatrol();
            
            yield return new WaitForSeconds(shootAfterInSeconds);

        }
        
    }

/*
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.magenta;
        
        Gizmos.DrawSphere(transform.position,detectionRadius);
        
    }
    */
}
