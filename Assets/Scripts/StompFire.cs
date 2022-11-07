using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompFire : MonoBehaviour
{
    [SerializeField] private float damageAmount;

    [SerializeField] private bool isFireActive;

    [SerializeField] private SpriteRenderer indicatorSpriteRenderer;
    
    [SerializeField] private ParticleSystem fireFx;


    private void Start()
    {
        IndicateAndFire();
    }

    private void IndicateAndFire()
    {
        StartCoroutine(IndicateAndFireInternal());
    }

    private IEnumerator IndicateAndFireInternal()
    {
        indicatorSpriteRenderer.enabled = true;

        yield return new WaitForSeconds(0.3f);

        indicatorSpriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.3f);
        
        indicatorSpriteRenderer.enabled = true;

        yield return new WaitForSeconds(0.3f);
        
        indicatorSpriteRenderer.enabled = false;
        
        yield return new WaitForSeconds(0.3f);
        
        isFireActive = true;
        
        fireFx.Play();

        Destroy(gameObject,fireFx.main.duration);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isFireActive) return;
        
        if (other.transform.TryGetComponent(out Health hp))
        {
            hp.ModifyHealth(-damageAmount);
            isFireActive = false;
        }
    }
}
