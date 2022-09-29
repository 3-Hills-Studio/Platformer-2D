using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Collectable Data", menuName = "New Collectable Data")]
public class CollectableData : ScriptableObject
{
    [field: SerializeField] public Sprite IconImage { get; set; }

    [field: SerializeField] public string Name { get; set; }

}
