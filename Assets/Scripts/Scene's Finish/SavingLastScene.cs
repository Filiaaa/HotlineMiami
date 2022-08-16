using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingLastScene : MonoBehaviour
{
    public int sceneIndex;
    void Start()
    {
        PlayerPrefs.SetInt("lastSceneIndex", sceneIndex);
    }


}
