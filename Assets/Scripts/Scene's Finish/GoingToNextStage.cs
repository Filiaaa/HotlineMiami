using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingToNextStage : MonoBehaviour
{
    public GameObject levelClosingAnim;
    public GameObject[] enemies;
    public GameObject thing;
    public bool haveThing = false;
/*    public Transform playersCanvas;*/
    public GameObject /*warningText,*/ player;
    public Sprite loadingSceneSprite;
    /*    public int loadingSceneID;
        public int sceneGoingID;*/
    public Vector3 newPos;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
/*                var warning_text = Instantiate(warningText, playersCanvas);*/
/*                StartCoroutine(DestroyText(warning_text));*/
                return;
            }
        }
        if (haveThing && thing == null || !haveThing)
        {
            levelClosingAnim.SetActive(true);
            StartCoroutine(StartOpenAnim());
        }
    }

/*    IEnumerator DestroyText(GameObject text)
    {
        yield return new WaitForSeconds(0.95f);
        Destroy(text);
    }
*/
    IEnumerator StartOpenAnim()
    {
        yield return new WaitForSeconds(0.3f);
        player.transform.position = newPos;
        Destroy(levelClosingAnim);
        /*LoadScene.backGround = loadingSceneSprite;
        LoadScene.sceneId = sceneGoingID;
        SceneManager.LoadScene(loadingSceneID);*/
    }
}
