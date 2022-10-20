using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
    [SerializeField] private float randomDestroyFireStart;
    [SerializeField] private float randomDestroyFireEnd;

    [SerializeField] private float damage;

    private bool _canDamagePlayer = true;
    
    void Start()
    {
        Destroy(gameObject,Random.Range(randomDestroyFireStart,randomDestroyFireEnd));       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_canDamagePlayer && other.gameObject.TryGetComponent(out Health health))
        {
            _canDamagePlayer = false;
            health.ModifyHealth(-damage);
            StartCoroutine(ResetCanDamagePlayer());
        }
    }

    private IEnumerator ResetCanDamagePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamagePlayer = true;
    }
}
