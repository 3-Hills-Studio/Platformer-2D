using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel5Health : Health
{
    [SerializeField] private ParticleSystem deathFx;
    [SerializeField] private ExitPortal exitPortal;

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    public override void ModifyHealth(float amount)
    {
        CurrentHealth += amount;

        ConfigCurrentHealth();

        HealthSlider.value = CurrentHealth / MaxHealth;
        
        if (CurrentHealth <= 0)
        {
            spriteRenderer.enabled = false;
           
            deathFx.Play();
            exitPortal.gameObject.SetActive(true);
        }
    }
}
