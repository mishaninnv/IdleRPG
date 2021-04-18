using System;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private SavedData _savedData;
    private string _savePath;
    private string _saveFileName = "savedParameters.json";
    private GameController _gameController;
    
    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif
    }

    void Start()
    {
        _gameController = GameController.GetInstance;
        _gameController.AfterEnemyDeathEvent += SaveToFile;
    }

    private void SaveToFile()
    {
        GetSaveData();
        var json = JsonUtility.ToJson(_savedData, true);
    
        try
        {
            File.WriteAllText(_savePath, json);
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void GetSaveData()
    {
        var playerProperties = _gameController.Player.CharacterProperties;
        
        _savedData.heroName = playerProperties.Name;
        _savedData.agility = playerProperties.Agility;
        _savedData.strength = playerProperties.Strength;
        _savedData.vitality = playerProperties.Vitality;
        _savedData.heroLvl = playerProperties.Level;
        _savedData.experience = _gameController.gameResources.Experience;
        _savedData.money = _gameController.gameResources.Money;
    }
}
