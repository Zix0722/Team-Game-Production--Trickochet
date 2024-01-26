using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpStars : MonoBehaviour
{
    GameObject theGame;
    Animator anim;
    public float AnimSeconds = 0.5f;
    GameObject HUD;
    GameObject theApp;
    bool isWork = true;
    // Start is called before the first frame update
    void Start()
    {
        theGame = GameObject.Find("Game");
        HUD = GameObject.Find("HUD");
        theApp = GameObject.Find("App");
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isWork)
        {
            return;
        }
        if (collision.gameObject.CompareTag("projectile"))
        {
            isWork = false;
            theGame.GetComponent<gameCommon>().numPickedStar++;
            theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().pickUpStar(transform);
            collision.gameObject.GetComponent<projectile>().hitCount++;
            if(theApp)
            {
                theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.starSpin);
            }
            Destroy(gameObject, 1f);
            StartCoroutine(playAnim());

        }
    }

    IEnumerator playAnim()
    {
        anim.SetBool("play", true);
        Debug.Log("play anim");
        yield return new WaitForSeconds(AnimSeconds);
        anim.SetBool("play", false);
        HUD.GetComponent<HUDcontroller>().getStar();
        
    }
}
