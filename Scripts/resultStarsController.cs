using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultStarsController : MonoBehaviour
{
    Animator anim1;
    Animator anim2;
    Animator anim3;

    GameObject theGame;
    private void OnEnable()
    {
        theGame = GameObject.Find("Game");
        anim1 = transform.GetChild(0).GetComponent<Animator>();
        anim2 = transform.GetChild(1).GetComponent<Animator>();
        anim3 = transform.GetChild(2).GetComponent<Animator>();
        StartCoroutine(showStarAnims());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator showStarAnims()
    {
        if(theGame)
        {
            int starsNum = theGame.GetComponent<gameCommon>().numPickedStar;
            switch(starsNum)
            {
                case 0:

                    break;
                case 1:
                    anim1.enabled = true;
                    break;
                case 2:
                    anim1.enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    anim2.enabled = true;
                    break;
                case 3:
                    anim1.enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    anim2.enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    anim3.enabled = true;
                    break;
            }
        }
    }
}
