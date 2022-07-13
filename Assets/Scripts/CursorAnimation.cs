using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    public Vector2 offset;
    public Texture2D[] Cursors;
    private float Timer = 1f;
    private int Temp = 0;

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Temp >= Cursors.Length) Temp = 0;
        if (Timer <= 0)
        {
            Cursor.SetCursor(Cursors[Temp], offset, CursorMode.ForceSoftware);
            Temp++;
            Timer = 0.1f; // 0.04f = Speed
        }
    }
}
