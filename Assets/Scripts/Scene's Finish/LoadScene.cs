using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoadScene : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public Image loadBar;
    public Text barTxt;
    public int sceneId;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneCor());
    }

    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(sceneId);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            loadBar.fillAmount = progress;
            barTxt.text = string.Format("{0:0}%", progress * 100f);
            yield return 0;
        }
    }
}
