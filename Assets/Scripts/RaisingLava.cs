using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingLava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            Destroy(player.gameObject);
        }
    }
}
