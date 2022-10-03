using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : BaseCollectable
{
    
    public override void Collected(Player player)
    {
        Player = player;
        StoreInInventory();
    }

    public override void StoreInInventory()
    {
        gameObject.SetActive(false);
        GamePlayEvents.RaiseOnAddCollectable(this);
    }

    public override void Use()
    {
        Debug.Log("USED STAMINA POTION!");
        Destroy(gameObject);
    }
}
