using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel5Health : Health
{
    [SerializeField] private ParticleSystem deathFx;
    [SerializeField] private ExitPortal exitPortal;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject healthCanvas;
    
    
    [SerializeField] private BossLevel3Shooting bossLevel3Shooting;
    
    
    public override void ModifyHealth(float amount)
    {
        CurrentHealth += amount;

        ConfigCurrentHealth();

        HealthSlider.value = CurrentHealth / MaxHealth;
        
        if (CurrentHealth <= 0)
        {
            spriteRenderer.enabled = false;
            healthCanvas.gameObject.SetActive(false);
            
            bossLevel3Shooting.StopShooting();
            
            deathFx.Play();
            exitPortal.gameObject.SetActive(true);
        }
    }
}
