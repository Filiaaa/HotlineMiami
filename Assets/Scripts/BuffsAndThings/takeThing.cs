using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeThing : MonoBehaviour
{

    public GameObject pressBtnMenu, filledCircle, Player;
    public float timeToTake, playerSpeedDown;
    float time = 0;
    bool onTrig = false;

/*    void Start()
    {
        filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 0);
    }
*/
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
            time += Time.deltaTime;

            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360 * time / timeToTake);
            if (time >= timeToTake)
            {
                pressBtnMenu.SetActive(false);
                print("taken");
                Player.GetComponent<PlayerMover>().movingSpeed -= playerSpeedDown;
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            time = 0; 
            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            time = 0;
            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360);
            onTrig = false;
            pressBtnMenu.SetActive(false);
        }
    }

/*    private void OnTriggerStay2D(Collider2D collision)
    {
        print("tr");
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            time += Time.deltaTime;

            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360 * time / timeToTake); 
            if (time >= timeToTake)
            {
                pressBtnMenu.SetActive(false);
                print("taken");
                gameObject.SetActive(false);
            }
        }
    }*/
}
