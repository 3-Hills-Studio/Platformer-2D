using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenBullet : MonoBehaviour
{

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Health health))
        {
            health.ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }
}
