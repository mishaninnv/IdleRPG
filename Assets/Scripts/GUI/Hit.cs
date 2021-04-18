using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hit : MonoBehaviour
{
    public Text damageTxt;

    public IEnumerator ShowHit(int damage)
    {
        damageTxt.text = damage.ToString();
        StartCoroutine(MoveUp());
        yield return new WaitForSeconds(1);
        StopCoroutine(MoveUp());
        Destroy(gameObject);
    }

    private IEnumerator MoveUp()
    {
        while (true)
        {
            transform.position += Time.deltaTime * Vector3.up;
            yield return null;
        }
    }
}
