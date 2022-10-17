using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel3 : Boss
{
    [field: SerializeField] public float MoveThreshold { get; set; }
    
    [field: SerializeField] public float AttackThreshold { get; set; }
    
    [field: SerializeField] public Animator Animator { get; set; }

    [field: SerializeField] public Transform AttackPoint { get; set; }
    
    [field: SerializeField] public float AttackRadius { get; set; }

    private const string AttackTriggerName = "Attack";


    private bool IsWithinAttackRange =>
        Vector2.Distance(transform.position, Player.transform.position) <= AttackThreshold;
    
    private void Update()
    {

        if (Math.Abs(Player.transform.position.y - transform.position.y) > 2f || Vector2.Distance(transform.position,Player.transform.position) > MoveThreshold) return;
        
        RotateTowardsPlayer();

        if (!IsWithinAttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(Player.transform.position.x,transform.position.y),MoveSpeed * Time.deltaTime);
        }
        
        if (IsWithinAttackRange)
        {
            Animator.SetTrigger(AttackTriggerName);
        }
        
    }

    public bool DamagePlayer()
    {
        var rHit = Physics2D.CircleCast(AttackPoint.position, AttackRadius, Vector2.zero, 0, PlayerLayerMask);

        if (rHit)
        {
            if (rHit.transform.gameObject.TryGetComponent(out Health health))
            {
                health.ModifyHealth(-Damage);
                return true;
            }
            return false;
        }
        return false;
    }
    
    private void RotateTowardsPlayer()
    {
        if (transform.position.x < Player.transform.position.x)
        {
            transform.localScale = Vector2.one;
            return;
        }

        transform.localScale = new Vector2(-1,1);
    }
}
