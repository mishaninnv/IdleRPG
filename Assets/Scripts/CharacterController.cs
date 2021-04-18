using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Hit hitImage;
    public AudioSource ShootSound;
    
    internal Canvas canvas;
    internal Character CharacterProperties;
    internal Animator Animator;
    internal AnimationClip AttackClip;
    internal GameController GameController;

    internal virtual void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        Animator = GetComponent<Animator>();
        CharacterProperties = GetComponent<Character>();
        GameController = GameController.GetInstance;
        
        SetEvents();

        AttackClip = GameController.FindAnimationClip(gameObject, "Attack");
    }

    private void ToAttack()
    {
        StartCoroutine("Attack");
    }

    public IEnumerator Attack()
    {
        var enemy = GetEnemy();
        
        while (true)
        {
            Animator.speed = CharacterProperties.AttackSpeed;
            Animator.SetBool("isAttack", true);

            var currAttack = Random.Range(CharacterProperties.MinAttack, CharacterProperties.MaxAttack);
            enemy.TakeDamage(currAttack);
            
            if(ShootSound != null) ShootSound.Play();
            
            yield return new WaitForSeconds(AttackClip.length / Animator.speed);
            Animator.speed = 1;
            Animator.SetBool("isAttack", false);
            yield return new WaitForSeconds(.3f);
        }
    }

    private CharacterController GetEnemy()
    {
        return gameObject.name.Equals("Player") ? GameController.Enemy : GameController.Player as CharacterController;
    }

    protected virtual void TakeDamage(int damage)
    {
        CharacterProperties.Health -= damage;

        ShowHit(damage);
        
        var oldHp = CharacterProperties.Health + damage;
        var maxHp = CharacterProperties.MaxHealth;
        var currHp = CharacterProperties.Health; 
        
        StartCoroutine(CharacterProperties.characterWindowGUI.HealthBarUpdate(oldHp, maxHp, currHp));
    }

    private void ShowHit(int damage)
    {
        var position = gameObject.transform.position + Vector3.up * 1.5f;
        var hit = Instantiate(hitImage, position, Quaternion.identity, canvas.transform);
        StartCoroutine(hit.ShowHit(damage));
    }

    private void StopAttackCoroutines()
    {
        StopCoroutine("Attack");
    }

    private void SetDefaultAnimatorSpeed()
    {
        Animator.speed = 1;
    }

    internal virtual void SetEvents()
    {
        GameController.StartFightEvent += ToAttack;
        GameController.BeforeEnemyDeathEvent += StopAttackCoroutines;
        GameController.BeforeEnemyDeathEvent += SetDefaultAnimatorSpeed;
    }

    internal void DeletedEventMethods()
    {
        GameController.StartFightEvent -= ToAttack;
        GameController.BeforeEnemyDeathEvent -= StopAttackCoroutines;
        GameController.BeforeEnemyDeathEvent -= SetDefaultAnimatorSpeed;
    }
}