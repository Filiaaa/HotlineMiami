using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject levelClosingAnim;
    public GameObject[] EnemiesToKill;
    public int nextSceneId;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < EnemiesToKill.Length; i++)
        {
            if (EnemiesToKill[i] != null)
            {
                /*                var warning_text = Instantiate(warningText, playersCanvas);*/
                /*                StartCoroutine(DestroyText(warning_text));*/
                return;
            }
        }
        levelClosingAnim.SetActive(true);
        StartCoroutine(StartOpenAnim());

    }

    IEnumerator StartOpenAnim()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(nextSceneId);
        /*LoadScene.backGround = loadingSceneSprite;
        LoadScene.sceneId = sceneGoingID;
        SceneManager.LoadScene(loadingSceneID);*/
    }
}
