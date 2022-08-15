using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    public Sprite chosedSprite;
    public Sprite unChosedSprite;
    public SpriteRenderer[] otherLanguages;
    public string languageName;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void changeLanguage()
    {
        PlayerPrefs.SetString("language", languageName);
        sr.sprite = chosedSprite;
        for (int i = 0; i < otherLanguages.Length; i++)
        {
            otherLanguages[i].sprite = otherLanguages[i].gameObject.GetComponent<SetLanguage>().unChosedSprite;
        }
    }
}
