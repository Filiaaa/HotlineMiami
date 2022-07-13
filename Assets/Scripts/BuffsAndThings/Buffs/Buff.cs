using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public PlayerMover player;
    public float secondsToDefault;
    public GameObject buffIcon, iconCanvas;
    public Text timer;

    

    public virtual void Use()
    {

    }

    public virtual IEnumerator ToDefaultSettings()
    {
        while (true)
        {

        }
    }
}
