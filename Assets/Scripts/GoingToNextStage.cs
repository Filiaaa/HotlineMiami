using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingToNextStage : MonoBehaviour
{
    public GameObject levelClosingAnim;
    public GameObject[] enemies;
    bool allEnemiesDie = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
                return;
            }
        }
        levelClosingAnim.SetActive(true);
        StartCoroutine(StartOpenAnim());
    }

    IEnumerator StartOpenAnim()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
