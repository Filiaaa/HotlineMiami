using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 offsetInHands;
    public Sprite spriteInHands;

    public virtual void Attack()
    {

    }


    public void Take()
    {
        transform.localPosition = offsetInHands;
        /*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
    }
}
