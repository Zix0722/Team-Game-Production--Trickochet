using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityMoving : MonoBehaviour
{
    public bool isVerticalMoving = false;
    public bool isHorizontalMoving = false;
    public bool isFromOriginalPos = false;

    public float movingDistance = 3.0f;
    public float movingSpeed = 1.0f;
   
    public bool reverse = false;  // to-do fix
    public bool isSwitchControlled = false;
    

    Vector3 originalPos;
    Vector3 posEnd;
    Vector3 posStart;

    bool isTouchEnd = false;
    [HideInInspector]
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = !isSwitchControlled;
        originalPos = transform.position;
        posEnd = transform.position;
        posStart = transform.position;

        if (reverse)
        {
            movingSpeed = -movingSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            if (isVerticalMoving)
            {
                doVerticalMoving();

            }
            if (isHorizontalMoving)
            {
                doHorizontalMoving();
            }
        }
        
    }

    void doVerticalMoving()
    {

        
        
        if (isFromOriginalPos)
        {

            if (reverse)
            {
                posEnd.y = originalPos.y - movingDistance;

                if (transform.position.y > posEnd.y && !isTouchEnd)
                {

                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y + movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.y < originalPos.y)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y - movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            else
            {
                posEnd.y = originalPos.y + movingDistance;

                if (transform.position.y < posEnd.y && !isTouchEnd)
                {

                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y + movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.y > originalPos.y)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y - movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
           

            



        }
        else
        {
            if (reverse)
            {
                posEnd.y = originalPos.y - movingDistance;
                posStart.y = originalPos.y + movingDistance;

                if (transform.position.y > posEnd.y && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y + movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.y < posStart.y)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y - movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            else
            {
                posEnd.y = originalPos.y + movingDistance;
                posStart.y = originalPos.y - movingDistance;

                if (transform.position.y < posEnd.y && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y + movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.y > posStart.y)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y - movingSpeed * Time.deltaTime,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            

            
        }
    
    }

    void doHorizontalMoving()
    {
        if (isFromOriginalPos)
        {

            if (reverse)
            {
                posEnd.x = originalPos.x - movingDistance;

                if (transform.position.x > posEnd.x && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x + movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.x < originalPos.x)
                {
                    transform.position = new Vector3(transform.position.x - movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            else
            {
                posEnd.x = originalPos.x + movingDistance;

                if (transform.position.x < posEnd.x && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x + movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.x > originalPos.x)
                {
                    transform.position = new Vector3(transform.position.x - movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }

            



        }
        else
        {
            if(reverse)
            {
                posEnd.x = originalPos.x - movingDistance;
                posStart.x = originalPos.x + movingDistance;

                if (transform.position.x > posEnd.x && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x + movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.x < posStart.x)
                {
                    transform.position = new Vector3(transform.position.x - movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            else
            {
                posEnd.x = originalPos.x + movingDistance;
                posStart.x = originalPos.x - movingDistance;

                if (transform.position.x < posEnd.x && !isTouchEnd)
                {
                    transform.position = new Vector3(transform.position.x + movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = true;
                }
                if (isTouchEnd && transform.position.x > posStart.x)
                {
                    transform.position = new Vector3(transform.position.x - movingSpeed * Time.deltaTime,
                                                     transform.position.y,
                                                     transform.position.z);
                }
                else
                {
                    isTouchEnd = false;
                }
            }
            

            
        }
    }
}
