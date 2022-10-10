using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataController : MonoBehaviour
{
    public static SaveDataController Singleton;

    public const string FILE_NAME = "platformer.2d";

    public SaveData loadedSaveData;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        LoadSavedFile();
        //LoadData();
    }

    private void LoadSavedFile()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath,FILE_NAME)))
        {
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, FILE_NAME), FileMode.Open);

            if (file.Length > 0)
            {

                BinaryFormatter bf = new BinaryFormatter();

                try
                {
                    loadedSaveData = (SaveData) bf.Deserialize(file);
                }
                catch (Exception e)
                {
                    Debug.Log("An error occured while loading save data.");
                }
                
                file.Close();
                return;
            }
        }
    }

    public void LoadData()
    {
        if (loadedSaveData == null) return;
        
        GameController.singleton.currentCoinAmount = loadedSaveData.coins;

        LoadSceneName();

        SceneController.Singleton.loadedPlayerPosition =
            new Vector2(loadedSaveData.playerPosX, loadedSaveData.playerPosY);
    }

    private void LoadSceneName()
    {
        if (loadedSaveData.lastSceneName == null)
        {
            SceneController.Singleton.lastSceneName = "Level 1";
            return;
        }
        SceneController.Singleton.lastSceneName = loadedSaveData.lastSceneName;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, FILE_NAME));

        SaveGameData(saveData);

        bf.Serialize(file,saveData);
        
        file.Close();
        
        Debug.Log("GAME SAVED!");

    }

    private void SaveGameData(SaveData saveData)
    {
        saveData.coins = GameController.singleton.currentCoinAmount;
        saveData.lastSceneName = "Level " + (SceneManager.GetActiveScene().buildIndex);
        
        saveData.playerPosX = GameController.singleton.currentActivePlayer.transform.position.x;
        saveData.playerPosY = GameController.singleton.currentActivePlayer.transform.position.y;

        saveData.playerHp = GameController.singleton.currentActivePlayer.Health.CurrentHealth;

    }

}
