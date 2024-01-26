using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardEnermy : MonoBehaviour
{
    public float delayOftheVanish = 1f;
    BoxCollider2D bC2D;
    public bool isCactus = false;
    public int HP = 1;
    [HideInInspector]
    public bool isTouchSheild = false;
    GameObject theGame;
    GameObject theScore;
    GameObject theApp;
    GameObject HUD;
    // Start is called before the first frame update
    void Start()
    {
        bC2D = GetComponent<BoxCollider2D>();
        theGame = GameObject.Find("Game");
        theScore = GameObject.Find("ScoreSystem");
        HUD = GameObject.Find("HUD");
        theApp = GameObject.Find("App");
        if (isCactus)
        {
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(HP == 1)
        {
            if(!isCactus)
            {
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                return;
            }
        }
       
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile"))
        {
            
            HP -= 1;
            
            if (HP == 0)
            {
                
                {
                    theGame.GetComponent<cameraShake>().doLMidShake();
                    theScore.GetComponent<ScoreSystem>().killAEnemy(collision.transform);
                    StartCoroutine(destoryProjectileOrEnermy(collision));
                }
            }
            else
            {
                if(isCactus)
                {
                    collision.gameObject.GetComponent<projectile>().hitCount++;
                    theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.cactusGetDmg);
                    transform.GetChild(0).GetComponent<Animator>().SetTrigger("getDamage");
                    //HUD.GetComponent<HUDcontroller>().killEnemy();
                    //Destroy(collision.gameObject);
                }
            }
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            HP -= 1;
            //do animations
           
            
            
            
        }
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator destoryProjectileOrEnermy(Collider2D collision)
    {
        theGame.GetComponent<gameCommon>().enermiesList.Remove(gameObject);
        yield return new WaitForEndOfFrame();
        if (collision.gameObject.CompareTag("projectile"))
        {

            if (isTouchSheild)
            {
                Destroy(collision.gameObject);
                Debug.Log("destroy projectile");
                isTouchSheild = false;
            }
            else
            {
                if (gameObject)
                {
                    if (isCactus)
                    {
                        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<Animator>().SetBool("isDead", true);
                    }
                    HUD.GetComponent<HUDcontroller>().killEnemy();
                    if (theGame.GetComponent<gameCommon>().theApp)
                    {
                        if (!theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
                        {
                            if (!isCactus)
                            {
                                gameObject.GetComponent<AudioSource>().Play();
                            }
                            else
                            {
                                theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.cactusGetDmg);
                            }
                        }
                    }
                    if(isCactus)
                    {
                        yield return new WaitForSeconds(delayOftheVanish + 1.5f);
                        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else
                    {
                        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        yield return new WaitForSeconds(delayOftheVanish);
                        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    }

                    collision.gameObject.GetComponent<projectile>().hitCount++;
                    float seed = Mathf.Sin(Time.time);
                    if (seed >= 0f)
                    {
                        HUD.GetComponent<HUDcontroller>().playBlinking();
                    }
                    else
                    {
                        HUD.GetComponent<HUDcontroller>().playHappy();
                    }
                    //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.layer = 7;
                   // Destroy(gameObject,1.5f);

                }

            }

            //toDo Anim
        }
    }

}
