using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    
    [field: SerializeField] public int MeleeDamage { get; protected  set; }
    
    [SerializeField] protected LayerMask groundLayer;
    
    [SerializeField] protected BoxCollider2D wallCheckCollider;
    
    [SerializeField] protected float moveSpeed;
    
    public int MaxHealth { get; protected set; }
    
    public abstract void Die();
    
}
