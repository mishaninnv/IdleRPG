using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<EnemyController> enemies;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.GetInstance;
        _gameController.AfterEnemyDeathEvent += CreateEnemy;
    }

    public void CreateEnemy()
    {
        StartCoroutine(CreateEnemyCoroutine());
    }

    private IEnumerator CreateEnemyCoroutine()
    {
        yield return null;
        var number = Random.Range(0, enemies.Count);
        var createEnemy = enemies[number];
        var enemy = Instantiate(createEnemy);
        yield return null;
        enemy.CharacterProperties.Name = createEnemy.name;
        enemy.StartCoroutine("ChangePosition");
        _gameController.Enemy = enemy;
        _gameController.MainMenu.UpdateEnemyStats();
    }
}
