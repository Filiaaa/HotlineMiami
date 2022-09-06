using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScreenMode : MonoBehaviour
{
    public Dropdown screenModes;

    void Start()
    {

        screenModes.value = PlayerPrefs.GetInt("screenMode");
        switch (screenModes.value)
        {
            case 0:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.MaximizedWindow);
                break;

        }
    }

    public void SetScreenMode_()
    {
        PlayerPrefs.SetInt("screenMode", screenModes.value);
        switch (screenModes.value)
        {
            case 0:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.MaximizedWindow);
                break;

        }
    }


}
