using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class inputSystem : MonoBehaviour
{
    
    public List<GameObject> shootingObjs;
    GameObject newShootingObj;

    float clickedCount = 0;
    float clickedTime = 0;
    public float clickedDelay = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q is pressed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        mouseUpdate();
        //touchUpdate();
       
    }
    void unactivateOtherShootingObjs()
    {
        foreach(var shootingPos in shootingObjs)
        {
            var child = shootingPos.transform.GetChild(0);
            child.gameObject.SetActive(false);
            shootingPos.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void mouseUpdate()
    {
        if (doubleClick())
        {
            Debug.Log("Double Click");
        }
    }

    void touchUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && Input.touchCount == 1)
            {
                if(Input.GetTouch(0).tapCount == 2 )
                {
                    // do double click event
                }
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    if (hit.transform.gameObject.CompareTag("shootingPos"))
                    {
                        selectPos(hit);
                    }
                }
            }
        }
    }

    void selectPos(RaycastHit hit)
    {
        newShootingObj = hit.transform.gameObject;
        if(!newShootingObj.GetComponent<shooting>().isLocked)
        {
            unactivateOtherShootingObjs();
            var activate = newShootingObj.transform.GetChild(0);
            activate.gameObject.SetActive(true);
            //activate.GetComponent<LineRenderer>().enabled = false;
            newShootingObj.transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }

   bool doubleClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickedCount++;
            StartCoroutine(SingleClickCheck());

            if (clickedCount == 1)
            {
                clickedTime = Time.time;
            }
        }
        if(clickedCount > 1 && Time.time - clickedTime < clickedDelay)
        {
            clickedCount = 0;
            clickedTime = 0;
           
            return true;
        }
        
        
        
        return false;
    }


    IEnumerator SingleClickCheck()
    {
        yield return new WaitForSeconds(0.05f);
    }
}
