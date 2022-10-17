using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float randomDestroyFireStart;
    [SerializeField] private float randomDestroyFireEnd;

    void Start()
    {
        Destroy(gameObject,Random.Range(randomDestroyFireStart,randomDestroyFireEnd));       
    }

}
