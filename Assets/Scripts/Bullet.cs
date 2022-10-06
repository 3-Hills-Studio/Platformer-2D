using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private int damage;
    
    void Start()
    {
        Destroy(gameObject,2.5f);
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
        
        if (col.gameObject.TryGetComponent(out PatrollingEnemy enemy))
        {
            enemy.Health -= damage;

            if (enemy.Health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            
            Destroy(gameObject);
        }
    }
    
    
}
