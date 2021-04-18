using System.Collections;
using UnityEngine;

public class EnemyController : CharacterController
{
    public CharacterWindowGUI enemyWindowGUI;
    
    internal override void Start()
    {
        base.Start();
        
        CharacterProperties.characterWindowGUI = Instantiate(enemyWindowGUI, canvas.transform);
    }

    private IEnumerator ChangePosition()
    {
        while (true)
        {
            var newPosition = new Vector3(
                transform.position.x - Time.deltaTime * 0.5f,
                transform.position.y,
                transform.position.z);

            transform.position = newPosition;

            if (transform.position.x <= 1)
            {
                GameController.StartFight();
                break;
            }

            yield return null;
        }
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CharacterProperties.Health <= 0)
        {
            GameController.BeforeEnemyDeath();
            StartCoroutine("EnemyDeathCoroutine");
        }
    }

    private IEnumerator EnemyDeathCoroutine()
    {
        Animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(1);
        
        GameController.AfterEnemyDeath();
        
        DeletedEventMethods();
        Destroy(CharacterProperties.characterWindowGUI.gameObject);
        Destroy(gameObject);
    }
}