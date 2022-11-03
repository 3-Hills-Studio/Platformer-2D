using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvl6 : Boss
{
    [SerializeField] private Animator animator;

    [SerializeField] private float minMoveSpeed;
    
    [SerializeField] private float maxMoveSpeed;

    [SerializeField] private BossLevel6Shooting bossLevel6Shooting;

    [SerializeField] private List<Transform> walkingPoints;

    [SerializeField] private Transform raycastPoint;

    [SerializeField] private LayerMask groundLayerMask;

    private bool _phaseTwoStarted = false;

    private Vector2 groundPosition;
    
    public int currentIndex = 0;

    private int RandomIndex => Random.Range(0, walkingPoints.Count);
    private float RandomSpeed => Random.Range(minMoveSpeed, maxMoveSpeed);
    
    public virtual void Start()
    {
        /*
        transform.position = walkingPoints[RandomIndex].position;
        currentIndex = RandomIndex;
        StartPatrol();
        bossLevel6Shooting.StartShootingPlayer();
        */

        //faza 2
        DetectGround();
        //lerpuj na taj ground
        //pocni sa loopom na fazu 2 

    }

    private IEnumerator LerpToGround()
    {
        float speed = RandomSpeed;
        
        while (Mathf.Abs(transform.position.y - groundPosition.y) > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, groundPosition, speed * Time.deltaTime);

            yield return null;
        }
    }

    private void DetectGround()
    {
        var rHit = Physics2D.Raycast(raycastPoint.position, Vector2.down, Mathf.Infinity,groundLayerMask);

        if (rHit)
        {
            groundPosition = rHit.point;
            StartCoroutine(LerpToGround());
        }
    }

    public virtual void StartPatrol()
    {
        StartCoroutine(PatrolLoop());
    }
    
    public IEnumerator PatrolLoop()
    {
        float moveSpeed = RandomSpeed;
        
        while (gameObject.activeSelf)
        {
            //if (Mathf.Abs(transform.position.x - walkingPoints[currentIndex].position.x) > Mathf.Epsilon)
            if (Vector2.Distance(transform.position,walkingPoints[currentIndex].position) > Mathf.Epsilon)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    walkingPoints[currentIndex].position,
                    moveSpeed * Time.deltaTime);

                yield return null;
                    
                continue;
            }

            currentIndex = RandomIndex;
            moveSpeed = RandomSpeed;
            
            yield return null;

        }
    }
    
}
