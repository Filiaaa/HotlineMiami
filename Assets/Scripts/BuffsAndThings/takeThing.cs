using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeThing : MonoBehaviour
{

    public GameObject pressBtnMenu, filledCircle, Player;
    public GameObject[] enemiesToTake;
    public GameObject[] spawnEnemies;
    public float timeToTake, playerSpeedDown;
    float time = 0;
    bool onTrig = false;
    PlayerMover playerMover;

    void Start()
    {
        playerMover = Player.GetComponent<PlayerMover>();
    }

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
        if (onTrig && Input.GetKey(KeyCode.E) && playerMover.lastNearestCollisionThing == gameObject)
        {
            for (int i = 0; i < spawnEnemies.Length; i++)
            {
                spawnEnemies[i].SetActive(true);
            }
            time += Time.deltaTime;

            filledCircle.GetComponent<Renderer>().material.SetFloat("_Arc1", 360 * time / timeToTake);
            if (time >= timeToTake)
            {
                pressBtnMenu.SetActive(false);
                print("taken");
                Player.GetComponent<PlayerMover>().movingSpeed -= playerSpeedDown;
                for (int i = 0; i < enemiesToTake.Length; i++)
                {
                    if (enemiesToTake[i] != null)
                    {
                        /*                var warning_text = Instantiate(warningText, playersCanvas);*/
                        /*                StartCoroutine(DestroyText(warning_text));*/
                        return;
                    }
                }
                Destroy(gameObject);
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
