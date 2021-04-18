using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public float showTime;
    
    private Transform _backGround;
    private Text _moneyTxt;
    private Text _expTxt;

    private void Start()
    {
        _backGround = transform.GetChild(0);
        _moneyTxt = _backGround.GetChild(0).GetComponent<Text>();
        _expTxt = _backGround.GetChild(1).GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public IEnumerator ShowReward(int money, int exp)
    {
        _moneyTxt.text = $"Gold: {money}";
        _expTxt.text = $"Exp: {exp}";
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}
