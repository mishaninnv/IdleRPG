public class PlayerController : CharacterController
{
    //TODO: сделать получение этих значений из Animator и не выносить в отдельный метод
    private bool isFight { get; set; }
    private bool isWin { get; set; }

    private void FightEvent()
    {
        isFight = !isFight;
        Animator.SetBool("isFight", isFight);
    }
    
    private void WinnerEvent()
    {
        isWin = !isWin;
        Animator.SetBool("isWin", isWin);
    }

    internal override void SetEvents()
    {
        base.SetEvents();
        GameController.StartFightEvent += FightEvent;
        GameController.BeforeEnemyDeathEvent += WinnerEvent;
        GameController.AfterEnemyDeathEvent += WinnerEvent;
        GameController.AfterEnemyDeathEvent += FightEvent;
    }
}