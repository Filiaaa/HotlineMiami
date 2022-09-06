using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    public static int cursor_numb = 1;
    public Vector2 offset1, offset2, offset3, offset4, offset5;
    public Texture2D[] Cursors_fisrst, Cursors_second, Cursors_fird, Cursors_fourth, Cursors_fifth;
    private float Timer_first = 0.1f, Timer_second = 0.1f, Timer_fird = 0.1f, Timer_fourth = 0.1f, Timer_fifth = 0.1f;
    private int Temp1 = 0, Temp2 = 0, Temp3 = 0, Temp4 = 0, Temp5 = 0;

    void Update()
    {
        PlayerPrefs.SetInt("cursor", cursor_numb);
        switch (cursor_numb)
        {
            case 1:
                {
                    Timer_first -= Time.deltaTime;
                    if (Temp1 >= Cursors_fisrst.Length) Temp1 = 0;
                    if (Timer_first <= 0)
                    {
                        Cursor.SetCursor(Cursors_fisrst[Temp1], offset1, CursorMode.ForceSoftware);
                        Temp1++;
                        Timer_first = 0.1f; // 0.04f = Speed
                    }
                    break;
                }
            case 2:
                {
                    Timer_second -= Time.deltaTime;
                    if (Temp2 >= Cursors_second.Length) Temp2 = 0;
                    if (Timer_second <= 0)
                    {
                        Cursor.SetCursor(Cursors_second[Temp2], offset2, CursorMode.ForceSoftware);
                        Temp2++;
                        Timer_second = 0.1f; // 0.04f = Speed
                    }
                    break;
                }
            case 3:
                {
                    Timer_fird -= Time.deltaTime;
                    if (Temp3 >= Cursors_fird.Length) Temp3 = 0;
                    if (Timer_fird <= 0)
                    {
                        Cursor.SetCursor(Cursors_fird[Temp3], offset3, CursorMode.ForceSoftware);
                        Temp3++;
                        Timer_fird = 0.1f; // 0.04f = Speed
                    }
                    break;
                }
            case 4:
                {
                    Timer_fourth -= Time.deltaTime;
                    if (Temp4 >= Cursors_fourth.Length) Temp4 = 0;
                    if (Timer_fourth <= 0)
                    {
                        Cursor.SetCursor(Cursors_fourth[Temp4], offset4, CursorMode.ForceSoftware);
                        Temp4++;
                        Timer_fourth = 0.1f; // 0.04f = Speed
                    }
                    break;
                }
            case 5:
                {
                    Timer_fifth -= Time.deltaTime;
                    if (Temp5 >= Cursors_fifth.Length) Temp5 = 0;
                    if (Timer_fifth <= 0)
                    {
                        Cursor.SetCursor(Cursors_fifth[Temp5], offset5, CursorMode.ForceSoftware);
                        Temp5++;
                        Timer_fifth = 0.1f; // 0.04f = Speed
                    }
                    break;
                }
        }
    }
    

    public void ChangeCursor(int cursorID)
    {
        cursor_numb = cursorID;
    }
}
