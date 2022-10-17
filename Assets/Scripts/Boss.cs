using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; set; }
    
    [field: SerializeField] public float Damage { get; set; }
    
    [field: SerializeField] public LayerMask PlayerLayerMask { get; set; }
    
    protected Player Player => GameController.singleton.currentActivePlayer;
}
