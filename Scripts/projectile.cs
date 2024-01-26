using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    GameObject theGame;
    GameObject theScore;
    GameObject HUD;

    public const int bounceTime = 10000;
    bool m_canBounce = true;
    int m_haveBounced = 0;
    Rigidbody2D m_rb2D;
    public Vector3 m_direction;
    public float projectileSpeed = 2.0f;

    public bool isTrackBall = false;

    bool isDead = false;

    public int hitCount = 0;

    Vector2 justTouchPos;
    Vector2 bounceBackPos;
    GameObject shootingObj;
    Vector2 bulletHolePos;
    public List<Sprite> bulletHoles;
    public GameObject bulletHolePrefab;
    private AudioSource audio;

    public bool isDoingCurve = false;
    float seed;
    // Start is called before the first frame update
    void Start()
    {
        theGame = GameObject.Find("Game");
        theScore = GameObject.Find("ScoreSystem");
        HUD = GameObject.Find("HUD");
        theGame.GetComponent<gameCommon>().isThereAmmoInAir = true;
        shootingObj = GameObject.Find("activate");
        m_rb2D = GetComponent<Rigidbody2D>();
        m_rb2D.velocity = m_direction * projectileSpeed;
        audio = GetComponent<AudioSource>();

        if(theGame.GetComponent<gameCommon>().theApp)
        {
            if(theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
            {
                audio.enabled = false;
            }
        }

        if (!isTrackBall)
        {
            StartCoroutine(getReady());
            
        }

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(!isTrackBall)
        {
            seed = Random.Range(0f, 2f);
            seed = Mathf.Sin(Time.time);

            if (theGame.GetComponent<gameCommon>().theApp)
            {
                if (!theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
                {
                    audio.enabled = true;
                }
            }
        }

        //---------------------------------------------------------------------------------------------
        if (m_haveBounced == bounceTime)
        {
            m_canBounce = false;
        }

        if (!isDead)
        {
            float angle = Mathf.Atan2(m_rb2D.velocity.y, m_rb2D.velocity.x) * Mathf.Rad2Deg;
            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        //---------------------------------------------------------------------------------------------


    }

    private void FixedUpdate()
    {
        if (isTrackBall)
        {
            StartCoroutine(delayDestroy());
        }
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isTrackBall)
        {
            justTouchPos = transform.position;
            StartCoroutine(delayRecordPos());
        }
        else
        {
            if (collision.gameObject.CompareTag("softWall"))
            {
                stopMoving();

                if (theGame.GetComponent<gameCommon>().theApp)
                {
                    if (!theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
                    {
                        Debug.Log(collision.gameObject);
                        collision.gameObject.GetComponent<AudioSource>().Play();
                    }
                }
                if(hitCount == 0)
                {
                    //StartCoroutine(changeMood(HUDcontroller.sanchoMood.sad));
                    Debug.Log(seed);
                    if (seed >= 0f)
                    {
                        HUD.GetComponent<HUDcontroller>().playAngry();
                    }
                    else
                    {
                        HUD.GetComponent<HUDcontroller>().playSad();
                    }
                }
                return;   // to-do Animation
            }
            doBouncement();
            if (collision.gameObject.CompareTag("wall"))
            {
                if (theGame.GetComponent<gameCommon>().theApp)
                {
                    if (!theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
                    {
                        collision.gameObject.GetComponent<AudioSource>().Play();
                    }
                }

                theScore.GetComponent<ScoreSystem>().Ricochet(transform);
                theGame.GetComponent<cameraShake>().doLightShake();
                //StartCoroutine(theGame.GetComponent<cameraShake>().doLight());
            }
            if(collision.gameObject.CompareTag("enemy"))
            {
                hitCount++;
                if (hitCount == 1)
                {

                    //StartCoroutine(changeMood(HUDcontroller.sanchoMood.angry));

                    
                    if (seed >= 0f)
                    {
                        HUD.GetComponent<HUDcontroller>().playBlinking();  
                    }
                    else
                    {
                        HUD.GetComponent<HUDcontroller>().playHappy();
                    }
                }
                if (hitCount >= 2)
                {
                    //StartCoroutine(changeMood(HUDcontroller.sanchoMood.shocked));
                    HUD.GetComponent<HUDcontroller>().playShocked();
                }

            }

        }
        
    }

    private void doBouncement()
    {
        if (m_canBounce)
        {
            m_haveBounced++;
        }
        else
        {
            stopMoving();
        }
    }

    private void stopMoving()
    {
        isDead = true;
        m_rb2D.velocity = new Vector2(0.0f, 0.0f);
        transform.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.layer = 7;  // dead layer
        theGame.GetComponent<gameCommon>().isThereAmmoInAir = false;

        bulletHolePos = transform.GetChild(1).position;
        var bulletHole = Instantiate(bulletHolePrefab, bulletHolePos, transform.rotation);
        int randNum = Random.Range(0, bulletHoles.Count - 1);
        bulletHole.GetComponent<SpriteRenderer>().sprite = bulletHoles[randNum];
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject,2f);
    }


   IEnumerator getReady()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.layer = 10; // bounce layer
        if(isTrackBall)
        {
            gameObject.layer = 8; // track ball
        }
    }

    IEnumerator delayRecordPos()
    {
        yield return new WaitForSeconds(0.05f);
        bounceBackPos = transform.position;
        shootingObj.GetComponent<AimingLineController>().justTouchPos = justTouchPos;
        shootingObj.GetComponent<AimingLineController>().bounceBackPos = bounceBackPos;
        Destroy(gameObject);
    }
    IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        bounceBackPos = transform.position;
        shootingObj.GetComponent<AimingLineController>().justTouchPos = justTouchPos;
        shootingObj.GetComponent<AimingLineController>().bounceBackPos = bounceBackPos;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(!isTrackBall && theGame)
        {
            if(theGame.GetComponent<gameCommon>().isEmpty)
            {

            }
            else
            {
               theGame.GetComponent<gameCommon>().isThereAmmoInAir = false;
            }
        }
    }

    public IEnumerator changeMood(HUDcontroller.sanchoMood mood)
    {
        yield return new WaitForSeconds(0.1f);
        theGame.GetComponent<gameCommon>().HUD.GetComponent<HUDcontroller>().currentMood = mood;
        yield return new WaitForSeconds(1.5f);
        theGame.GetComponent<gameCommon>().HUD.GetComponent<HUDcontroller>().currentMood = HUDcontroller.sanchoMood.blinking;
    }

}
