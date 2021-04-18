using UnityEngine;
using UnityEngine.UI;

public class GameResources : MonoBehaviour
{
    public Text moneyTxt;
    public Image experienceImage;
    
    private int _money;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameController.GetInstance;
    }

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            moneyTxt.text = Money.ToString();
        }
    }

    public int Experience { get; set; }
    public int MaxExperience { get; set; }

    public void AddExperience(int experience)
    {
        Experience += experience;

        if (Experience > MaxExperience)
        {
            Experience -= MaxExperience;
            _gameController.IncreaseLevel();
        }

        experienceImage.fillAmount = (float) Experience / MaxExperience;
    }
}