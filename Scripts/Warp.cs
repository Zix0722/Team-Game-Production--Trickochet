using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public bool front = false;
    public bool back = false;
    public bool left = false;
    public bool right= false;
    public float AnimSeconds = 1.0f;
    public GameObject otherWarp;
    bool ignoreNextTrigger = false;
    Vector3 enermyPos;

    Vector3 frontDirection;
    Vector3 backDirection;
    Vector3 leftDirection;
    Vector3 rightDirection;
    touchingSide result;
    GameObject theGame;
    GameObject theApp;
    Animator anim;
    AudioSource theAudio;
    enum touchingSide
    {
        front,
        back,
        left,
        right
    }
    // Start is called before the first frame update
    void Start()
    {
        theGame = GameObject.Find("Game");
        theApp = GameObject.Find("App");
        anim = GetComponent<Animator>();
        theAudio = GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().sortingOrder = 5;
    }

    // Update is called once per frame
    void Update()
    {
        enermyPos = transform.position;

        frontDirection = transform.up;
        backDirection = -transform.up;
        leftDirection = -transform.right;
        rightDirection = transform.right;

        Vector3 m_frontlLine_end = enermyPos + frontDirection * 2.0f;
        Vector3 m_backLine_end = enermyPos + backDirection * 2.0f;
        Vector3 m_leftLine_end = enermyPos + leftDirection * 2.0f;
        Vector3 m_rightLine_end = enermyPos + rightDirection * 2.0f;

        Debug.DrawLine(enermyPos, m_frontlLine_end, Color.green);
        Debug.DrawLine(enermyPos, m_backLine_end, Color.yellow);
        Debug.DrawLine(enermyPos, m_leftLine_end, Color.red);
        Debug.DrawLine(enermyPos, m_rightLine_end, Color.black);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile"))
        {
            if(ignoreNextTrigger)
            {
                ignoreNextTrigger = false;
                StartCoroutine(doOutput(collision));
                return;
            }

            Vector3 shootingAt = collision.transform.position;
            Vector3 shootingDir = shootingAt - enermyPos;

            float max = -1f;


            if (Vector3.Dot(frontDirection, shootingDir) > max)
            {
                max = Vector3.Dot(frontDirection, shootingDir);
                result = touchingSide.front;
            }
            if (Vector3.Dot(leftDirection, shootingDir) > max)
            {
                max = Vector3.Dot(leftDirection, shootingDir);
                result = touchingSide.left;
            }
            if (Vector3.Dot(rightDirection, shootingDir) > max)
            {
                max = Vector3.Dot(rightDirection, shootingDir);
                result = touchingSide.right;
            }
            if (Vector3.Dot(backDirection, shootingDir) > max)
            {
                max = Vector3.Dot(backDirection, shootingDir);
                result = touchingSide.back;
            }

            if (result == touchingSide.front)
            {
                if (front)
                {
                    //do warp
                    if(collision.CompareTag("projectile"))
                    {
                        StartCoroutine(doWarp(collision));
                        otherWarp.GetComponent<Warp>().ignoreNextTrigger = true;
                        
                    }
                  
                    Debug.Log("hit front");
                }
            }
            if (result == touchingSide.back)
            {
                if (back)
                {
                    if (collision.CompareTag("projectile"))
                    {
                        //playAudio();
                        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.barrelsDrum); 
                    }
                    Vector2 dir = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    Vector2 Vn = Vector2.Dot(dir, backDirection) * backDirection;
                    Vector2 Vt = dir - Vn;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vt - Vn;

                }
                Debug.Log("hit back");
            }
            if (result == touchingSide.left)
            {
                if (left)
                {
                    if (collision.CompareTag("projectile"))
                    {
                       // playAudio();
                        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.barrelsDrum);
                    }
                    Vector2 dir = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    Vector2 Vn = Vector2.Dot(dir, leftDirection) * leftDirection;
                    Vector2 Vt = dir - Vn;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vt - Vn;
                }
                Debug.Log("hit left");
            }
            if (result == touchingSide.right)
            {

                if (right)
                {
                    if(collision.CompareTag("projectile"))
                    {
                        //playAudio();
                        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.barrelsDrum);
                    }    
                    Vector2 dir = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    Vector2 Vn = Vector2.Dot(dir, rightDirection) * rightDirection;
                    Vector2 Vt = dir - Vn;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vt - Vn;
                }
                Debug.Log("hit right");
            }

        }
    }

   IEnumerator doWarp(Collider2D collision)
    {
        anim.SetBool("play", true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.barrelsMagic);
        yield return new WaitForSeconds(0.1f);
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        collision.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(AnimSeconds);
        anim.SetBool("play", false);
        collision.gameObject.transform.position = otherWarp.transform.position;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<projectile>().projectileSpeed * otherWarp.transform.up;
    }
    IEnumerator doOutput(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        anim.SetBool("play", true);
        yield return new WaitForSeconds(AnimSeconds);
        anim.SetBool("play", false);
        collision.GetComponent<TrailRenderer>().enabled = true;
        collision.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<projectile>().projectileSpeed * transform.up;
    }
    void playAudio()
    {
        if (theGame.GetComponent<gameCommon>().theApp)
        {
            if (!theGame.GetComponent<gameCommon>().theApp.GetComponent<appSystem>().isMutedSoundEffect)
            {
                theAudio.Play();
            }
        }
        
    }
}
