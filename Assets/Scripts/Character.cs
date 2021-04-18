using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterWindowGUI characterWindowGUI;

    internal GameController _gameController;

    public string Name { get; set; }
    public float AttackSpeed { get; set; }
    public int MinAttack { get; set; }
    public int MaxAttack { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Vitality { get; set; }
    public int Level { get; set; }

    internal virtual void Start()
    {
        _gameController = GameController.GetInstance;
        
        StartCoroutine(Initialization());
    }

    private IEnumerator Initialization()
    {
        foreach (var row in ParametersLoadedFromTable.ParametersTable)
        {
            if (gameObject.name.Equals(row[0]))
            {
                Strength = int.Parse(row[1]);
                Agility = int.Parse(row[2]);
                Vitality = int.Parse(row[3]);
                Level = int.Parse(row[4]);
                break;
            }
        }

        yield return null;
        
        characterWindowGUI.SetName(Name);
    }
}