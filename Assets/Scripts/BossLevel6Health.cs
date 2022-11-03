using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel6Health : Health
{
    [SerializeField] private ParticleSystem deathFx;
    
    [SerializeField] private GameObject healthCanvas;
    
    public override void ModifyHealth(float amount)
    {
        CurrentHealth += amount;

        ConfigCurrentHealth();

        HealthSlider.value = CurrentHealth / MaxHealth;
        
        if (CurrentHealth <= 0)
        {
            healthCanvas.gameObject.SetActive(false);

            //deathFx.Play();
        }
    }
}