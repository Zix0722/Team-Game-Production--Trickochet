using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    public GameObject outputBlackhole;
    private GameObject inputBlackhole;

    [HideInInspector]
    public bool isAsOutput = false;
    
    public float AnimTime = 1.0f;
    [HideInInspector]
    public Vector2 projectileVelocity;
    [HideInInspector]
    public Vector2 noteDownPos;
    [HideInInspector]
    public Vector3 noteDownScale;
    bool isFinishedAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        inputBlackhole = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("projectile") && !isAsOutput)
        {
            outputBlackhole.GetComponent<blackHole>().isAsOutput = true;
            var velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            
            outputBlackhole.GetComponent<blackHole>().projectileVelocity = velocity;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(slowlyInto(collision));
            
        }
        else if(collision.gameObject.CompareTag("projectile"))
        {
            StartCoroutine(slowlyComeOut(collision));
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isFinishedAnim && !isAsOutput)
        {
            collision.transform.position = outputBlackhole.transform.position; 
        }
        if (isFinishedAnim && isAsOutput)
        {
            collision.transform.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isFinishedAnim = false;
        isAsOutput = false;
        projectileVelocity = Vector2.zero;
        noteDownPos = Vector2.zero;
        noteDownScale = Vector3.zero;
    }
    

    IEnumerator slowlyInto(Collider2D collision)
    {
        Vector2 targetPos = inputBlackhole.transform.position;
        noteDownPos = (Vector2) collision.transform.position;
        outputBlackhole.GetComponent<blackHole>().noteDownPos = noteDownPos - targetPos;
        noteDownScale = collision.gameObject.transform.localScale;
        outputBlackhole.GetComponent<blackHole>().noteDownScale = noteDownScale;

        int targetFrame = (int)(AnimTime * 60);
        float eachMoveTakeTime = AnimTime / targetFrame;
        
        Vector3 eachFrameMove = (targetPos - noteDownPos) / targetFrame;
        Vector3 eachFrameScale = (Vector3.zero - noteDownScale) / targetFrame;

        for (int i = 0; i < targetFrame; i++ )
        {
            collision.transform.position += eachFrameMove;
            
            collision.gameObject.transform.localScale += eachFrameScale;
            
            yield return new WaitForSeconds(eachMoveTakeTime);
        }
        isFinishedAnim = true;
        
    }


    IEnumerator slowlyComeOut(Collider2D collision)
    {
        Vector2 targetPos = -noteDownPos + (Vector2) transform.position;
        var currentPos = (Vector2)collision.transform.position;

        
        int targetFrame = (int)(AnimTime * 60);
        float eachMoveTakeTime = AnimTime / targetFrame;
        Vector3 eachFrameScale = noteDownScale / targetFrame;
        Vector3 eachFrameMove = (targetPos - currentPos) / targetFrame;

        for (int i = 0; i < targetFrame; i++)
        {
            collision.transform.position += eachFrameMove;

            collision.gameObject.transform.localScale += eachFrameScale;

            yield return new WaitForSeconds(eachMoveTakeTime);
        }
        isFinishedAnim = true;

    }

}
