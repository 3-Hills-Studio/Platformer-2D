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
    
    private float RandomPauseForAttack => Random.Range(minPhaseTwoPauseForAttack, maxPhaseTwoPauseForAttack);

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Transform _lastPointDash;


    [SerializeField] private float minPhaseTwoPauseForAttack;
    [SerializeField] private float maxPhaseTwoPauseForAttack;

    [SerializeField] private GameObject stompFirePrefab;
    
    [SerializeField] private Transform stompFireYTransform;

    [SerializeField] private int stompAttackCount = 0;
    [SerializeField] private int maxStompAttackCount = 0;
    
    
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

        //StartCoroutine(DashPhaseTwo());
        StartCoroutine(DashPhaseThree());
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

    public IEnumerator DashPhaseTwo()
    {
        _lastPointDash = pointA;

        float dashSpeed = maxMoveSpeed * 2;
        
        var target = GetNewTarget();

        float currentTime = Time.time;

        float timeToFireAfter = 1;
        
        while (gameObject.activeSelf)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,dashSpeed * Time.deltaTime);

            if (Time.time - currentTime > timeToFireAfter)
            {
                bossLevel6Shooting.FireInCircle();
                currentTime = Time.time;
            }
            
            if (Mathf.Abs(target.x - transform.position.x) < Mathf.Epsilon)
            {
                target = GetNewTarget();
                
                bossLevel6Shooting.StartShootingPlayer();
                
                yield return new WaitForSeconds(RandomPauseForAttack);
                
                bossLevel6Shooting.StopShootingPlayer();
            }

            yield return null;
        }
    }
    
    public IEnumerator DashPhaseThree()
    {
        _lastPointDash = pointA;

        float dashSpeed = maxMoveSpeed * 2;
        
        var target = GetNewTarget();

        float currentTime = Time.time;

        float timeToFireAfter = 1;
        
        while (gameObject.activeSelf)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,dashSpeed * Time.deltaTime);
            
            if (Mathf.Abs(target.x - transform.position.x) < Mathf.Epsilon)
            {
                target = GetNewTarget();
            }
            
            if (Time.time - currentTime > timeToFireAfter + 2)
            {
                StompAttack();
                currentTime = Time.time;
                yield return new WaitForSeconds(2);
            }
           
            yield return null;
        }
    }

    private void StompAttack()
    {
        
        Vector2 playerPosition = GameController.singleton.currentActivePlayer.transform.position;

        playerPosition.y = stompFireYTransform.position.y;

        stompAttackCount += 1;
        
        for (int i = 0; i < stompAttackCount; i++)
        {
            if (i == 0)
            {
                Instantiate(stompFirePrefab, playerPosition, Quaternion.identity);
                continue;
            }

            playerPosition.x = Random.Range(pointA.position.x, pointB.position.x);
            
            Instantiate(stompFirePrefab, playerPosition, Quaternion.identity);
        }

        if (stompAttackCount > maxStompAttackCount)
        {
            stompAttackCount = 0;
        }

    }

    private Vector2 GetNewTarget()
    {
        if (_lastPointDash != null && _lastPointDash == pointA)
        {
            _lastPointDash = pointB;
            
            return _lastPointDash.position;
        }
        _lastPointDash = pointA;
        
        return _lastPointDash.position;
    }

}
