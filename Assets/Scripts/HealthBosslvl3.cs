using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBosslvl3 : Health
{
    
    [field: SerializeField] public BoxCollider2D OpenDoorsTrigger { get; set; }

    protected override void Update()
    {
        return;
    }

    public override void ModifyHealth(float amount)
    { 
        CurrentHealth += amount;

        ConfigCurrentHealth();

        HealthSlider.value = CurrentHealth / MaxHealth;
        
        if (CurrentHealth <= 0)
        {
            //death fx
            Debug.Log("enable door");
            OpenDoorsTrigger.enabled = true;
            Destroy(gameObject);
        }
    }
}
