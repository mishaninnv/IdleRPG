using System.Collections.Generic;
using UnityEngine;

public class ParametersLoadedFromTable : MonoBehaviour
{
    public static List<string[]> ParametersTable { get; private set; }

    private void Awake()
    {
        LoadParameters();
    }

    private void LoadParameters()
    {
        var paramData = Resources.Load<TextAsset>("CharacterParameters");
        var data = paramData.text.Split('\n');
        ParametersTable = GetParametersTable(data);
    }

    private List<string[]> GetParametersTable(string[] data)
    {
        var loadTable = new List<string[]>();

        foreach (var row in data)
        {
            var currRow = row.Split(';');

            loadTable.Add(currRow);
        }

        return loadTable;
    }
}
