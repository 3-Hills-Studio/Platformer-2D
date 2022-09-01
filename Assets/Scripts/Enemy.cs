using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int Damage { get; set; }

    [SerializeField] private CircleCollider2D circleCollider2D;
    
    [SerializeField] private BoxCollider2D wallCheckCollider;

    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector2 pushForce;
    

    public int MaxHealth { get; private set; }

    private void Start()
    {
        MaxHealth = Health;
    }

    private void Update()
    {
        transform.Translate(transform.localScale.x * moveSpeed * Time.deltaTime,0,0);
        
        if (wallCheckCollider.IsTouchingLayers(groundLayer))
        {
            transform.localScale =
                new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Health health))
        {
            if (col.transform.position.y - transform.position.y >= circleCollider2D.bounds.size.y / 2)
            {
                Destroy(gameObject);
                return;
            }

            health.ModifyHealth(-Damage);
            
            health.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x * pushForce.x,pushForce.y),ForceMode2D.Impulse);
        }
    }
}
