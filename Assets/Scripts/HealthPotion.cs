using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseCollectable
{
    [field: SerializeField] public float HealthAmount { get; private set; }

    public override void Collected(Player player)
    {
        if (player.TryGetComponent(out Health health))
        {
            health.ModifyHealth(HealthAmount);
            Destroy(gameObject);
            Debug.Log("COLLECTED HEALTH POTION!");
        }
    }
}
