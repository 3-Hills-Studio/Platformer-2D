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
        
        if (col.gameObject.TryGetComponent(out Health playerHealth))
        {
            playerHealth.ModifyHealth(-damage);

            if (playerHealth.CurrentHealth <= 0)
            {
                Destroy(playerHealth.gameObject);
            }
            
            Destroy(gameObject);
        }
        
    }
    
    
}