using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menu, closingTransition;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            Pause();
        }
    }

    public void GoToMainMenu()
    {
        closingTransition.SetActive(true);
        StartCoroutine(WaitingTransition());
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }


    IEnumerator WaitingTransition()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
