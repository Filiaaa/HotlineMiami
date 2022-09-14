using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public GameObject movingSelectingWindow;
    public Button[] buttons;
    private int currentButton = 0;
    public float movingSpeed, scaleSpeed;

    


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentButton].onClick.Invoke();
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) )
        {
            StopAllCoroutines();
            currentButton++;
            currentButton %= buttons.Length;
            StartCoroutine(SelectBtn(new Vector3(buttons[currentButton].transform.position.x - 3, buttons[currentButton].transform.position.y, 1), new Vector3(buttons[currentButton].transform.localScale.x + 0.03f, buttons[currentButton].transform.localScale.y + 0.2f)));
/*            buttons[currentButton].onClick.Invoke();*/
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            StopAllCoroutines();
            currentButton--;
            if(currentButton == -1)
            {
                currentButton = buttons.Length - 1;
            }
            StartCoroutine(SelectBtn(new Vector3(buttons[currentButton].transform.position.x - 3, buttons[currentButton].transform.position.y, 1), new Vector3(buttons[currentButton].transform.localScale.x + 0.03f, buttons[currentButton].transform.localScale.y + 0.2f)));
            /*            buttons[currentButton].onClick.Invoke();*/
        }
    }

    IEnumerator SelectBtn(Vector3 targetPos, Vector3 targetScale)
    {
        while (Mathf.Abs(movingSelectingWindow.transform.position.y - targetPos.y) > Mathf.Epsilon || Mathf.Abs(movingSelectingWindow.transform.localScale.x - targetScale.x) > Mathf.Epsilon)
        {
            print("a");
            movingSelectingWindow.transform.position = Vector3.Lerp(movingSelectingWindow.transform.position, targetPos, movingSpeed);
            movingSelectingWindow.transform.localScale = Vector3.Lerp(movingSelectingWindow.transform.localScale, targetScale, scaleSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
