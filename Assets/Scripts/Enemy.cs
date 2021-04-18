public class Enemy : Character
{
    internal override void Start()
    {
        base.Start();

        Level = _gameController.Player.CharacterProperties.Level;
        SetAttackSpeed();
        SetMaxHP();
        SetMinAttack();
        SetMaxAttack();
        ReloadHpGui();
        ReloadLevelGui();
    }

    private void SetAttackSpeed() => AttackSpeed = Level * 0.05f + 1 + Agility * 0.01f;
    private void SetMaxHP() => MaxHealth = Strength * 25 + Level * 50 + Vitality * 150;
    private void SetMinAttack() => MinAttack = Strength * 5 + Level * 7;
    private void SetMaxAttack() => MaxAttack = Strength * 9 + Level * 7;
    
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
