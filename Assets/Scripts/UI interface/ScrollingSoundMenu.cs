using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSoundMenu : MonoBehaviour
{

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, 0, 200), transform.localPosition.z);
    }
}
