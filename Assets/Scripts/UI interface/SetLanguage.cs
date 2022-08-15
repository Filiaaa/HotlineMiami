using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetLanguage : MonoBehaviour
{
    public Sprite chosedSprite;
    public Sprite unChosedSprite;
    public Image[] otherLanguages;
    public int languageIndex;

    Image im;

    void Start()
    {
        im = GetComponent<Image>();
        if (PlayerPrefs.HasKey("language") && PlayerPrefs.GetInt("language") == languageIndex)
        {
            im.sprite = chosedSprite;
            for (int i = 0; i < otherLanguages.Length; i++)
            {
                otherLanguages[i].sprite = otherLanguages[i].gameObject.GetComponent<SetLanguage>().unChosedSprite;
            }
        }

    }

    public void changeLanguage()
    {
        PlayerPrefs.SetInt("language", languageIndex);
        im.sprite = chosedSprite;
        for (int i = 0; i < otherLanguages.Length; i++)
        {
            otherLanguages[i].sprite = otherLanguages[i].gameObject.GetComponent<SetLanguage>().unChosedSprite;
        }
    }
}
