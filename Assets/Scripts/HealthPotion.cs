using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    [field: SerializeField] public float HealthAmount { get; private set; }

    public void Collected(Player player)
    {
        if (player.TryGetComponent(out Health health))
        {
            health.ModifyHealth(HealthAmount);
            Destroy(gameObject);
            Debug.Log("COLLECTED HEALTH POTION!");
        }
    }
}
