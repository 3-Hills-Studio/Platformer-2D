using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    [field: SerializeField] public CollectableData Data { get; set; }
    
    [field: SerializeField] public Player Player { get; set; }
    
    [field: SerializeField] public Sprite ItemIconSprite { get; set; }

    public abstract void Collected(Player player);
    
    public abstract void StoreInInventory();
    
    public abstract void Use();
}
