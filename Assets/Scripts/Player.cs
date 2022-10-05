using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    
    
    //todo damage interface za playera 

    private void Start()
    {
        GameController.singleton.currentActivePlayer = this;

        if (!string.IsNullOrEmpty(SceneController.Singleton.lastSceneName) &&
            SceneController.Singleton.lastSceneName == SceneManager.GetActiveScene().name &&
            SceneController.Singleton.loadedPlayerPosition != Vector2.zero)
        {
            transform.position = SceneController.Singleton.loadedPlayerPosition;
        }
        
        SaveDataController.Singleton.SaveGame();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.TryGetComponent(out IDestructible destructible))
        {
            destructible.DestroyAndPlayFx();
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent(out BaseCollectable collectable))
        {
            collectable.Collect(this);
            return;
        }
    }


}
