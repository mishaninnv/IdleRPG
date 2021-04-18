using System;
using System.IO;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public SavedData LoadedData { get; private set; }
    
    private string _savePath;
    private const string SaveFileName = "savedParameters.json";

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, SaveFileName);
#endif
        
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_savePath)) return;

        try
        {
            var json = File.ReadAllText(_savePath);

            LoadedData = JsonUtility.FromJson<SavedData>(json);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
