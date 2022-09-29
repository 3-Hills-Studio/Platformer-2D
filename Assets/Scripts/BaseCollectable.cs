using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    [field: SerializeField] public CollectableData Data { get; set; }

    public abstract void Collected(Player player);
}
