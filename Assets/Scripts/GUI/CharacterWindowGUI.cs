using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindowGUI : MonoBehaviour
{
    public Image healthBarImage;
    public Image avatar;
    public Text characterName;
    public Text healthBarText;
    public Text levelText;

    public IEnumerator HealthBarUpdate(float oldHP, float maxHP, float currentHP)
    {
        SetHealthPoints(currentHP, maxHP);

        while (true)
        {
            if (currentHP < oldHP)
            {
                var speed = oldHP / currentHP;
                
                oldHP -= 1 + speed;
                healthBarImage.fillAmount = oldHP / maxHP;
                
                if(oldHP <= currentHP) break;
            }
            else
            {
                var speed = maxHP / currentHP;
                
                oldHP += 3 + speed;
                healthBarImage.fillAmount = oldHP / maxHP;
                
                if (oldHP >= currentHP) break;
            }
            
            yield return new WaitForSeconds(.01f);
        }
    }

    public void SetHealthPoints(float currentHP, float maxHP)
    {
        healthBarText.text = currentHP + " / " + maxHP;
    }

    public void SetLevel(float level)
    {
        levelText.text = "Level: " + level;
    }

    public void SetName(string name)
    {
        characterName.text = name;
    }
}