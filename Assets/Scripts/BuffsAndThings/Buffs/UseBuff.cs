using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBuff : MonoBehaviour
{
    public GameObject pressBtnMenu, filledCircle, Player;
    public float timeToTake, timeToEnableCol;
    float circleTime = 0;
    bool onTrig = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onTrig = true;
            pressBtnMenu.SetActive(true);
            pressBtnMenu.transform.position = gameObject.transform.position;
        }
    }

    private void Update()
    {
        if (onTrig && Input.GetKey(KeyCode.E))
        {
            circleTime += Time.deltaTime;

            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360 * circleTime / timeToTake);
            if (circleTime >= timeToTake)
            {
                pressBtnMenu.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                onTrig = false;
                StartCoroutine(ActivatingBuff());
                gameObject.GetComponent<Buff>().Use();
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            circleTime = 0;
            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            circleTime = 0;
            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360);
            onTrig = false;
            pressBtnMenu.SetActive(false);
        }
    }

    IEnumerator ActivatingBuff()
    {
        
        yield return new WaitForSeconds(timeToEnableCol);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
