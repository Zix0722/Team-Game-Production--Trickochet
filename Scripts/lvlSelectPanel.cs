using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlSelectPanel : MonoBehaviour
{

    Button backButton;
    GameObject theApp;
    public Button goLeft;
    public Button goRight;
    bool isLeft = true;
    Animator anim;
    [HideInInspector]
    public List<Vector3> ButtonsPos;
   
    public static int numOfLevels;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        backButton = GameObject.Find("backButton").GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonClick);
        theApp = GameObject.Find("App");
        goLeft.onClick.AddListener(OnLeftButtonClick);
        goRight.onClick.AddListener(OnRightButtonClick);
        numOfLevels = theApp.GetComponent<appSystem>().lvlTotalNum;
        goLeft.gameObject.SetActive(false);
        
        //GetAllOriginPosForAllButtons();
    }

    private void OnEnable()
    {
        GetAllOriginPosForAllButtons();
        if(theApp)
        {
             if(theApp.GetComponent<appSystem>().currentPageForLevelSelect == 2)
             {
                 SetSecPagePos();
                 isLeft = false;
                 goLeft.gameObject.SetActive(true);
                 goRight.gameObject.SetActive(false);
             }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void OnBackButtonClick()
    {
        if (!isLeft)
        {
            SetBackToOriginPos();
            isLeft = true;
            theApp.GetComponent<appSystem>().currentPageForLevelSelect = 1;
        }
        gameObject.SetActive(false);
        theApp.GetComponent<appSystem>().isFirstTimeEnter = true;
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        goLeft.gameObject.SetActive(false);
        goRight.gameObject.SetActive(true);
    }

    void OnLeftButtonClick()
    {
        if (isLeft)
        {

        }
        else
        {
            theApp.GetComponent<appSystem>().currentPageForLevelSelect = 1;
            anim.ResetTrigger("slideBack");
            anim.SetTrigger("slideBack");
            goLeft.gameObject.SetActive(false);
            goRight.gameObject.SetActive(true);
            isLeft = true;

        }
    }

    void OnRightButtonClick()
    {
        if (!isLeft)
        {

        }
        else
        {
            anim.ResetTrigger("slide");
            theApp.GetComponent<appSystem>().currentPageForLevelSelect = 2;
            anim.SetTrigger("slide");
            goLeft.gameObject.SetActive(true);
            goRight.gameObject.SetActive(false);
            isLeft = false;
            StartCoroutine(delayGetPos());
        }
    }

    void SetBackToOriginPos()
    {
        for (int i = 0; i < numOfLevels; i++)
        {
            transform.GetChild(i).transform.position = ButtonsPos[i];
        }
    }
    void GetAllOriginPosForAllButtons()
    {
        for(int i = 0; i < numOfLevels; i++)
        {
            ButtonsPos.Add(transform.GetChild(i).transform.position);
            transform.GetChild(i).GetComponent<buttonEnterLvl>().lvlIndex = i;

        }
    }

    void SetSecPagePos()
    {
        for (int i = 0; i < numOfLevels; i++)
        {
            transform.GetChild(i).transform.position = theApp.GetComponent<appSystem>().ButtonsPos2[i];
        }
    }

    void GetAllSecPageButtonsPos()
    {
        for (int i = 0; i < numOfLevels; i++)
        {
            theApp.GetComponent<appSystem>().ButtonsPos2.Add(transform.GetChild(i).transform.position);
            transform.GetChild(i).GetComponent<buttonEnterLvl>().lvlIndex = i;

        }
    }

    IEnumerator delayGetPos()
    {
        yield return new WaitForSeconds(1f);
        GetAllSecPageButtonsPos();
    }
}   
