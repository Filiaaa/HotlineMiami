using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToNextStage : MonoBehaviour
{
    public GameObject levelClosingAnim, levelOpeningAnim, Player, point;
    public float animatingTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelOpeningAnim.SetActive(false);
        levelClosingAnim.SetActive(true);
        StartCoroutine(StartOpenAnim());
    }

    IEnumerator StartOpenAnim()
    {
        yield return new WaitForSeconds(animatingTime);
        Player.transform.position = point.transform.position;
        levelClosingAnim.SetActive(false);

        levelOpeningAnim.SetActive(true);
    }
}
