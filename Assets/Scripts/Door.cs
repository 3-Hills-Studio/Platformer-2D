using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool canOpen = true;

    private Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Player player) && canOpen)
        {
            canOpen = false;
            transform.DOMove(new Vector3(transform.position.x,transform.position.y + 2),0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            transform.DOMove(startingPosition,0.5f);
        }
    }
}
