using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoors : MonoBehaviour
{
    List<GameObject> objectsInCol = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (objectsInCol.Count == 0)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetBool("open", true);
        }
        objectsInCol.Add(collision.gameObject);

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        objectsInCol.Remove(collision.gameObject);
        print(objectsInCol.Count);
        if (objectsInCol.Count == 0)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetBool("open", false);
        }
    }
}
