using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{

    [field: SerializeField] public Vector3 StartPos { get; set; }
    
    [field: SerializeField] public Vector3 EndPosition { get; set; }

    [field: SerializeField] public float FallDuration { get; set; }
    
    [field: SerializeField] public Transform RayOrigin { get; set; }
    
    [field: SerializeField] public BoxCollider2D BoxCollider2D { get; set; }

    [field: SerializeField] public LayerMask GroundLayerMask { get; set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        GetGroundPosition();
        
        MoveToGround();
    }

    private void GetGroundPosition()
    {
        var rHit = Physics2D.Raycast(RayOrigin.position, Vector2.down, Mathf.Infinity, GroundLayerMask);

        if (rHit)
        {
            EndPosition = new Vector3(transform.position.x, rHit.point.y + BoxCollider2D.bounds.extents.y + Mathf.Epsilon);
        }
    }

    private void MoveToGround()
    {
        StartCoroutine(Move());
        
        IEnumerator Move()
        {
            yield return new WaitForSeconds(0.5f);
            transform.DOMove(EndPosition, FallDuration).OnComplete(MoveToStartPosition).WaitForStart();
        }
    }
    
    private void MoveToStartPosition()
    {
        StartCoroutine(Move());
        
        IEnumerator Move()
        {
            yield return new WaitForSeconds(0.5f);
            transform.DOMove(StartPos, FallDuration).OnComplete(MoveToGround);
        }
    }

}
