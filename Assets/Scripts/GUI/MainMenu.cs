using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CharacterStatsGUI playerStats;
    public CharacterStatsGUI enemyStats;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.GetInstance;
        _gameController.AfterEnemyDeathEvent += UpdatePlayerStats; 
    }

    public void UpdatePlayerStats() => StartCoroutine(UpdatePlayerStatsCoroutine());
    public void UpdateEnemyStats() => StartCoroutine(UpdateEnemyStatsCoroutine());
    
    private IEnumerator UpdatePlayerStatsCoroutine()
    {
        while (true)
        {
            var player = _gameController.Player.CharacterProperties;

            if (player.Equals(null)) yield return null;
            
            playerStats.attackTxt.text = $"{player.MinAttack}-{player.MaxAttack}";
            playerStats.attackSpeedTxt.text = player.AttackSpeed.ToString();
            playerStats.strengthTxt.text = player.Strength.ToString();
            playerStats.agilityTxt.text = player.Agility.ToString();
            playerStats.vitalityTxt.text = player.Vitality.ToString();
            playerStats.experienceTxt.text = $"{_gameController.gameResources.Experience}/" +
                                             $"{_gameController.gameResources.MaxExperience}";
            break;
        }
    }

    private IEnumerator UpdateEnemyStatsCoroutine()
    {
        while (true)
        {
            var enemy = _gameController.Enemy.CharacterProperties;

            if (enemy.Equals(null)) yield return null;
            
            enemyStats.attackTxt.text = $"{enemy.MinAttack}-{enemy.MaxAttack}";
            enemyStats.attackSpeedTxt.text = enemy.AttackSpeed.ToString();
            enemyStats.strengthTxt.text = enemy.Strength.ToString();
            enemyStats.agilityTxt.text = enemy.Agility.ToString();
            enemyStats.vitalityTxt.text = enemy.Vitality.ToString();
            
            break;
        }
    }
}