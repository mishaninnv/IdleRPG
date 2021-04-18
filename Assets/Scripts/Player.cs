public class Player : Character
{
    internal override void Start()
    {
        base.Start();

        SetEvents();
        _gameController.SetBasicParameters();
    }

    private void ShowNameWindow()
    {
        
    }

    private void SetEvents()
    {
        _gameController.BeforeEnemyDeathEvent += SetFullHP;
        _gameController.SetBasicParametersEvent += SetAttackSpeed;
        _gameController.SetBasicParametersEvent += SetMaxHP;
        _gameController.SetBasicParametersEvent += SetMinAttack;
        _gameController.SetBasicParametersEvent += SetMaxAttack;
        _gameController.SetBasicParametersEvent += ReloadHpGui;
        _gameController.SetBasicParametersEvent += ReloadLevelGui;

    }
    
    private void SetAttackSpeed() => AttackSpeed = Level * 0.05f + 1 + Agility * 0.01f;
    private void SetMaxHP() => MaxHealth = Strength * 25 + Level * 50 + Vitality * 150;
    private void SetMinAttack() => MinAttack = Strength * 5 + Level * 10;
    private void SetMaxAttack() => MaxAttack = Strength * 9 + Level * 10;
    
    private void SetFullHP()
    {
        var oldHp = Health;
        var maxHp = MaxHealth;
        Health = maxHp;

        StartCoroutine(characterWindowGUI.HealthBarUpdate(oldHp, maxHp, maxHp));
    }
    
    private void ReloadHpGui()
    {
        Health = MaxHealth;
        characterWindowGUI.SetHealthPoints(Health, MaxHealth);
    }

    private void ReloadLevelGui()
    {
        characterWindowGUI.SetLevel(Level);
    }
}
