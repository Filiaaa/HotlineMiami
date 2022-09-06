using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetToggle : MonoBehaviour
{
    public Toggle[] toggles;
    

    void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if(i == PlayerPrefs.GetInt("cursor") - 1)
            {
                toggles[i].isOn = true;
            }
            else
            {
                toggles[i].isOn = false;
            }
        }
    }

    
}
