using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonParticles : MonoBehaviour
{
    public Sprite[] brokenThingSprite;
    public GameObject[] particles;
    public GameObject[] hitParticles;
    public GameObject[] otherrColliders;
    public int nunmberofHitsTobreak = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            nunmberofHitsTobreak--;
            foreach (GameObject otherCol in otherrColliders)
            {
                if (otherCol != gameObject)
                {
                    otherCol.GetComponent<SummonParticles>().nunmberofHitsTobreak--;
                }
            }
            if (nunmberofHitsTobreak == 0)
            {
                foreach (GameObject particle in particles)
                {
                    var partical = Instantiate(particle, particle.transform.position, Quaternion.identity, null);
                    partical.SetActive(true);
                }
                Destroy(collision.gameObject);
                foreach (GameObject otherCol in otherrColliders)
                {
                    otherCol.GetComponent<SummonParticles>().enabled = false;
                }
                if (brokenThingSprite.Length == 0)
                {
                    Destroy(gameObject.transform.parent.gameObject);
                }
                else
                {
                    gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite = brokenThingSprite[Random.Range(0, brokenThingSprite.Length - 1)];
                }

            }
            else
            {
                foreach (GameObject particle in hitParticles)
                {
                    var partical = Instantiate(particle, particle.transform.position, Quaternion.identity, null);
                    partical.SetActive(true);
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            if(collision.GetComponent<Bullet>() != null)
            {
                Destroy(collision.gameObject);
            }
            nunmberofHitsTobreak--;
            foreach (GameObject otherCol in otherrColliders)
            {
                if(otherCol != gameObject)
                {
                    otherCol.GetComponent<SummonParticles>().nunmberofHitsTobreak--;
                }
            }
            if (nunmberofHitsTobreak == 0)
            {
                foreach (GameObject particle in particles)
                {
                    var partical = Instantiate(particle, particle.transform.position, Quaternion.identity, null);
                    partical.SetActive(true);
                }
                foreach (GameObject otherCol in otherrColliders)
                {
                    otherCol.GetComponent<SummonParticles>().enabled = false;
                }
                if (brokenThingSprite.Length == 0)
                {
                    Destroy(gameObject.transform.parent.gameObject);
                }
                else
                {
                    gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite = brokenThingSprite[Random.Range(0, brokenThingSprite.Length)];
                }

            }
            else
            {
                foreach (GameObject particle in hitParticles)
                {
                    var partical = Instantiate(particle, particle.transform.position, Quaternion.identity, null);
                    partical.SetActive(true);
                }
            }
        }
    }
}
