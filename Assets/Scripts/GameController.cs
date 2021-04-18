using System.Collections;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Reward Reward;
    public Spawner spawner;
    public MainMenu MainMenu;
    public int startMoney;
    
    [HideInInspector] public GameResources gameResources;
    private LoadData _loadData;

    public delegate void EmptyDelegate();
    public event EmptyDelegate StartFightEvent;
    public event EmptyDelegate BeforeEnemyDeathEvent;
    public event EmptyDelegate AfterEnemyDeathEvent;
    public event EmptyDelegate SetBasicParametersEvent;
    //public event EmptyDelegate PlayerDeathEvent;
    
    public PlayerController Player { get; private set; }
    public EnemyController Enemy { get; set; }
    public static GameController GetInstance { get; private set; }

    private void Awake()
    {
        if (GetInstance == null)
        {
            GetInstance = this;
        }
        else
        {
            Debug.LogError("Attempted to assign second GameController.gameController");
        }
    }

    void Start()
    {
        _loadData = GetComponent<LoadData>();
        gameResources = GetComponent<GameResources>();
        Player = FindObjectOfType<PlayerController>();
        
        SetMethodsToEvents();
        
        gameResources.Money = startMoney;
        spawner.CreateEnemy();

        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return null;
        
        if(!string.IsNullOrEmpty(_loadData.LoadedData.heroName)) 
            StartCoroutine(SetLoadedData(_loadData.LoadedData));

        yield return null;
        
        SetBasicParameters(); 
            
        gameResources.AddExperience(0);
        Player.CharacterProperties.characterWindowGUI.SetLevel(Player.CharacterProperties.Level);
            
        MainMenu.UpdatePlayerStats();
    }

    private void SetMethodsToEvents()
    {
        BeforeEnemyDeathEvent += ChangeGameResources;
        BeforeEnemyDeathEvent += SetReward;
        SetBasicParametersEvent += SetMaxExp;
    }

    public void StartFight() => StartFightEvent?.Invoke();
    public void BeforeEnemyDeath() => BeforeEnemyDeathEvent?.Invoke();
    public void AfterEnemyDeath() => AfterEnemyDeathEvent?.Invoke();
    public void SetBasicParameters() => SetBasicParametersEvent?.Invoke();
    
    private void SetMaxExp() => gameResources.MaxExperience = Player.CharacterProperties.Level * 50 + 100;
    
    public void IncreaseLevel()
    {
        Player.CharacterProperties.Level += 1;
        SetBasicParameters();
    }

    private void ChangeGameResources()
    {
        var money = Enemy.CharacterProperties.Level;
        gameResources.Money += money;

        var exp = Enemy.CharacterProperties.Level * 2 + 15;
        gameResources.AddExperience(exp);
    }

    private void SetReward()
    {
        Reward.gameObject.SetActive(true);

        var money = Enemy.CharacterProperties.Level;
        var exp = Enemy.CharacterProperties.Level * 2 + 15;
        StartCoroutine(Reward.ShowReward(money, exp));
    }

    public AnimationClip FindAnimationClip(GameObject go, string animationName)
    {
        foreach (var animationClip in AnimationUtility.GetAnimationClips(go))
        {
            if (animationClip.name.Equals(animationName))
            {
                return animationClip;
            }
        }

        return null;
    }

    private IEnumerator SetLoadedData(SavedData loadedData)
    {
        while (true)
        {
            if (Player.CharacterProperties.Equals(null)) yield return null;
            
            Player.CharacterProperties.Name = loadedData.heroName;
            Player.CharacterProperties.Agility = loadedData.agility;
            Player.CharacterProperties.Strength = loadedData.strength;
            Player.CharacterProperties.Vitality = loadedData.vitality;
            Player.CharacterProperties.Level = loadedData.heroLvl;
            
            gameResources.Money = loadedData.money;
            gameResources.Experience = loadedData.experience;
            
            break;
        }
    }
}