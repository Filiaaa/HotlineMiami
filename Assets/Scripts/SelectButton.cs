using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Button[] buttons;
    private int currentButton = 0;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentButton != buttons.Length - 1)
        {
            currentButton++;
            buttons[currentButton].Select();
            buttons[currentButton].onClick.Invoke();
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentButton != 0)
        {
            currentButton--;
            buttons[currentButton].Select();
            buttons[currentButton].onClick.Invoke();
        }
    }
}
