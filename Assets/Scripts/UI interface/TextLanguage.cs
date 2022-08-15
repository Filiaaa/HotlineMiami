using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextLanguage : MonoBehaviour
{
    public string[] languages;
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (!PlayerPrefs.HasKey("language"))
        {
            PlayerPrefs.SetInt("language", 1);
        }
        text.text = languages[PlayerPrefs.GetInt("language")];
    }
}
