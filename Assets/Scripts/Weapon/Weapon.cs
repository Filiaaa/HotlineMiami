using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 offsetInHands;
    public Quaternion offseRotationtInHands;
    public Sprite spriteInHands;

    public virtual void Attack()
    {

    }


    public void Take()
    {
        transform.localPosition = offsetInHands;
        transform.localRotation = offseRotationtInHands;
        /*        GetComponent<SpriteRenderer>().sprite = spriteInHands;*/
    }
}
