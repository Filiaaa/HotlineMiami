using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject closingAnim;

    public void Play(){
        closingAnim.SetActive(true);
        StartCoroutine(ClosingAnim());
    }

    public void Quit(){
        Application.Quit();
    }


    IEnumerator ClosingAnim(){
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }
}
