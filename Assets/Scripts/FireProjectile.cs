using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : BouncyProjectile
{
    protected override IEnumerator Start()
    {
        yield break;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        SpawnFire();
        
        if (col.gameObject.TryGetComponent(out Health health))
        {
            health.ModifyHealth(-damage);
        }
        
        Destroy(gameObject);
    }
}
