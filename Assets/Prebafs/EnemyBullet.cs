using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private int damage;
    
    void Start()
    {
        Destroy(gameObject,2.5f);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
        
/*
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            enemy.Health -= damage;

            if (enemy.Health <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }
*/

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