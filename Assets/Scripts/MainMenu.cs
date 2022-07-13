using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider effectsVol, musicVol, enemyVol, playerVol, allVol;
    public GameObject closingAnim, settingsWindow;
    public AudioMixer audioMixer;

    private void Start()
    {
        print(Time.timeScale);
        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));
            effectsVol.value = PlayerPrefs.GetFloat("EffectsVolume");
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            musicVol.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("EnemysVolume"))
        {
            audioMixer.SetFloat("EnemysVolume", PlayerPrefs.GetFloat("EnemysVolume"));
            enemyVol.value = PlayerPrefs.GetFloat("EnemysVolume");
        }

        if (PlayerPrefs.HasKey("PlayerVolume"))
        {
            audioMixer.SetFloat("PlayerVolume", PlayerPrefs.GetFloat("PlayerVolume"));
            playerVol.value = PlayerPrefs.GetFloat("PlayerVolume");
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            allVol.value = PlayerPrefs.GetFloat("volume");
        }
    }

    public void Play(){
        closingAnim.SetActive(true);
        StartCoroutine(ClosingAnim());
    }

    public void Quit(){
        Application.Quit();
    }


    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
    }


    public void ChangeEffectsVolume()
    {
        audioMixer.SetFloat("EffectsVolume", effectsVol.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVol.value);
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicVol.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVol.value);
    }

    public void ChangeEnemysVolume()
    {
        audioMixer.SetFloat("EnemysVolume", enemyVol.value);
        PlayerPrefs.SetFloat("EnemysVolume", enemyVol.value);
    }

    public void ChangePlayerVolume()
    {
        audioMixer.SetFloat("PlayerVolume", playerVol.value);
        PlayerPrefs.SetFloat("PlayerVolume", playerVol.value);
    }

    public void ChangeAllVolume()
    {
        AudioListener.volume = allVol.value;
        PlayerPrefs.SetFloat("volume", allVol.value);
    }


    IEnumerator ClosingAnim(){
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }
}
