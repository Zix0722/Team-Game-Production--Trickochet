using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject projectileTrackBallPrefab;
    public bool isLockedByEnermy = false;
    public float shootingCD = 2.0f;
    [HideInInspector]
    public bool isLocked = false;

    Vector2 clickWorldPos;
    Vector2 shootingPos;
    
    bool m_canFire = false;
    float m_timmer = 0.0f;
    bool m_isInCD = false;
    bool m_isAmmoEmpty = false;
    [HideInInspector]
    public  bool isControllingAngle = false;

    float clickedCount = 0;
    float clickedTime = 0;
    float clickedOnShootingObjCnt = 0;
    public float clickedDelay = 0.35f;

    GameObject theGame;
    [HideInInspector]
    public Vector2 dir;
    [HideInInspector]
    public bool isAiming = false;

    private Quaternion currentRotation;
    GameObject AmmoHUD;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile"))
        {
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("shotHimself");
            Debug.Log("shoot himself");
        }
    }
    void Start()
    {
        theGame = GameObject.Find("Game");
        isLocked = isLockedByEnermy;
        shootingPos = transform.GetChild(0).position;
        transform.GetChild(0).GetComponent<LineRenderer>().enabled = false;
        AmmoHUD = GameObject.Find("remainAmmoPanel");
        transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;
        Debug.Log(transform.GetChild(0).transform.eulerAngles.z);
        Vector2 newVec;
        float degreesToRadiansScale = 3.14159265f / 180f;
        float radian = (transform.GetChild(0).transform.eulerAngles.z + 90f) * degreesToRadiansScale;
        newVec.x = Mathf.Cos(radian);
        newVec.y = Mathf.Sin(radian);

        dir = newVec;

    }
    private void FixedUpdate()
    {
       
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        isAiming = true;
        isControllingAngle = true;

        checkIfHaveEnoughAmmo();
        if (isLocked)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            var child = transform.GetChild(0);
            if (child.gameObject.active)
            {
                m_canFire = true;
            }
            else
            {
                m_canFire = false;
            }
            if(!theGame.GetComponent<gameCommon>().cannotControl)
            {
                mouseUpdate();
            }
            Debug.DrawLine(shootingPos, clickWorldPos, Color.yellow);
        }
        checkTimmer();
    }

    private void predict()
    {
        //transform.GetChild(0).GetComponent<PredictionManager>().predict(projectilePrefab, transform.position, dir);
    }

    bool IsPointerOverGameObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        return (eventSystem.IsPointerOverGameObject(fingerId)
            && eventSystem.currentSelectedGameObject != null);
    }

    //---------------------------------------------------------------------------------------------
    [System.Obsolete]
    void mouseUpdate()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.touchCount > 0)
        {
            if (IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Hit UI, Ignore Touch");
                return;
            }
            else
            {
                Debug.Log("Handle Touch");
                
            }
        }

        if (doubleClick()) // do shooting logic inside the function
        {

        }


    }
    //---------------------------------------------------------------------------------------------

    void touchUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {

                clickWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 dir = (clickWorldPos - shootingPos).normalized;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    
                }
                else
                {
                    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                        return;
                    doFire(dir);
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------

    
    public void doFire(Vector2 dir)
    {
        
    
        if (m_canFire)
        {
            if (projectilePrefab.gameObject.CompareTag("pierceing"))
            {
                var rotate = transform.GetChild(0).transform.rotation;
                //rotate.z -= 90.0f;
                GameObject pierceing = Instantiate(projectilePrefab, shootingPos, rotate);
                var script_p = pierceing.GetComponent<pierceing>();
                script_p.m_direction = dir;
            }
            else
            {
                if(!m_isInCD)
                {
                    if(m_isAmmoEmpty)
                    {
                        return;
                    }
                    GameObject projectile = Instantiate(projectilePrefab, shootingPos, transform.rotation);
                    var script = projectile.GetComponent<projectile>();
                    script.m_direction = dir;
                    m_isInCD = true;
                    theGame.GetComponent<gameCommon>().numAmmo--;
                    isAiming = false;
                    transform.GetChild(0).GetComponent<LineRenderer>().enabled = false;
                    transform.GetChild(1).GetComponent<Animator>().SetTrigger("Shoot");
                    StartCoroutine(AmmoHUD.GetComponent<displayAmmo>().ReloadAmmo());
                    theGame.GetComponent<cameraShake>().doStrongShake();
                    StartCoroutine(delayChangeIsAiming(shootingCD));
                    
                }
            }
        }
    }

    void checkTimmer()
    {
        m_timmer += Time.deltaTime;
        if(m_timmer >= shootingCD)
        {
            m_isInCD = false;
            m_timmer = 0.0f;
        }
    }


    void checkIfHaveEnoughAmmo()
    {
        m_isAmmoEmpty = theGame.GetComponent<gameCommon>().isEmpty;
    }

    [System.Obsolete]
    bool doubleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                clickedOnShootingObjCnt++;
                clickedCount = 0;
            }
            else
            {
                 clickedCount++;
                clickedOnShootingObjCnt = 0;
                

            }


            if (clickedCount == 1)
            {
                clickedTime = Time.time;
            }
            else if(clickedOnShootingObjCnt == 1)
            {
                clickedTime = Time.time;
            }
        }

        if (Input.GetMouseButton(0) && isAiming && isControllingAngle)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
            }
            else
            {
                clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = (clickWorldPos - shootingPos).normalized;

               
                float angle = Mathf.Atan2(clickWorldPos.y - shootingPos.y, clickWorldPos.x - shootingPos.x) * Mathf.Rad2Deg - 90.0f;

                transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        if (clickedCount > 1 && Time.time - clickedTime < clickedDelay)
        {
            clickedCount = 0;
            clickedTime = 0;
            if (transform.GetChild(0).gameObject.active && !isAiming)
            {
                isAiming = true;
                isControllingAngle = true;

                clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir = (clickWorldPos - shootingPos).normalized;

                float angle = Mathf.Atan2(clickWorldPos.y - shootingPos.y, clickWorldPos.x - shootingPos.x) * Mathf.Rad2Deg - 90.0f;

                transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                //StartCoroutine(delayChangeIsAiming());

            }
            return true;
        }
        else if(clickedOnShootingObjCnt > 1 && Time.time - clickedTime < clickedDelay)
        {
            clickedCount = 0;
            clickedTime = 0;
            if (isAiming)
            {

                //doFire(dir);
            }
        }
        else if(clickedCount > 2 || Time.time - clickedTime > clickedDelay || clickedOnShootingObjCnt > 2)
        {
            clickedCount = 0;
            clickedOnShootingObjCnt = 0;
        }



        return false;
    }


    IEnumerator SingleClickCheck()
    {
        yield return new WaitForSeconds(clickedDelay);
        if (clickedCount == 1)
        {
            //Debug.Log("do single click");
            isControllingAngle = true;
            clickedCount = 0;
        }
    }
    IEnumerator delayChangeIsAiming(float time)
    {
        yield return new WaitForSeconds(time);
        transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;
    }
}
