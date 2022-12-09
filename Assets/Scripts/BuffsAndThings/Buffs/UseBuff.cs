using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBuff : MonoBehaviour
{
    public GameObject pressBtnMenu, filledCircle, Player;
    public PlayerMover playerMover;
    public float timeToTake, timeToEnableCol;
    float circleTime = 0;
    bool onTrig = false;
    SpriteRenderer sr;
    public Sprite pickUpSprite, defaultSprite, spriteInTrig;
    Renderer filledCircleRenderer;

    private void Start()
    {
        filledCircleRenderer = filledCircle.GetComponent<Renderer>();
        sr = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            sr.sprite = spriteInTrig;
            onTrig = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= playerMover.distBetwThings)
            {
                pressBtnMenu.SetActive(true);
                pressBtnMenu.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, 0);
                playerMover.lastNearestCollisionThing = gameObject;
            }
        }
    }


    private void Update()
    {

        if (onTrig && Input.GetKey(KeyCode.E) && playerMover.lastNearestCollisionThing == gameObject)
        {
            sr.sprite = pickUpSprite;
            circleTime += Time.deltaTime;

            filledCircleRenderer.material.SetFloat("_Arc1", 360 * circleTime / timeToTake);
            if (circleTime >= timeToTake)
            {
                pressBtnMenu.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                onTrig = false;
                StartCoroutine(ActivatingBuff());
                gameObject.GetComponent<Buff>().Use();
            }
        }
        if (onTrig && Input.GetKeyUp(KeyCode.E))
        {
            sr.sprite = spriteInTrig;
            circleTime = 0;
            filledCircleRenderer.material.SetFloat("_Arc1", 360);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMover.distBetwThings = 10;
            sr.sprite = defaultSprite;
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
