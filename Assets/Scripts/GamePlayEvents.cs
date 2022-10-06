using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayEvents
{
    public static event Action<BaseCollectable> OnAddCollectable;
    
    public static event Action<BaseCollectable> OnRemoveItemFromInventory;
    
    public static void RaiseOnRemoveItemFromInventory(BaseCollectable collectable) => OnRemoveItemFromInventory?.Invoke(collectable);
    
    public static void RaiseOnAddCollectable(BaseCollectable collectable) => OnAddCollectable?.Invoke(collectable);
    

}
