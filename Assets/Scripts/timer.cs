using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timer : MonoBehaviour
{
    public PlayerMover player;
    public Image timerBar;
    public float time = 5;
    float timeLeft;
    void Start()
    {
    timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0 )
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / time;
        }
        else
        {
            player.KillPlayer();
        }
    }
}
