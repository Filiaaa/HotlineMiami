using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weed : Buff
{
    public float newTimeScale;

    public override void Use()
    {
        Time.timeScale = newTimeScale;
        var buffIcon_ = Instantiate(buffIcon, iconCanvas.transform);
        buffIcon_.AddComponent(GetComponent<Buff>().GetType());
        buffIcon_.GetComponent<Buff>().timer = buffIcon_.transform.GetChild(0).GetComponent<Text>();
        buffIcon_.GetComponent<Buff>().player = player;
        buffIcon_.GetComponent<Buff>().secondsToDefault = secondsToDefault;
        buffIcon_.GetComponent<Buff>().StartCoroutine("ToDefaultSettings");
        Destroy(gameObject);
    }

    public override IEnumerator ToDefaultSettings()
    {
        while (true)
        {
            if (int.Parse(timer.text) != 0)
            {
                secondsToDefault--;
                timer.text = secondsToDefault.ToString();
            }
            else
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
