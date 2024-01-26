using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class appSystem : MonoBehaviour
{
    public static GameObject theApp;
    public GameObject theRecentGame;
    public Button backMenuButton;
    public GameObject justEnterGamePanel;
    public GameObject lvlSelectPanel;
    public int lvlTotalNum = 12;
    public GameObject mainCamera;

    public GameObject pauseGamePanel;
    Button tapToStartButton;

    //[HideInInspector]
    public List<string> lvlName;
    //---------------------------------------------------use for back to correct page of select level
    public int currentPageForLevelSelect = 0;
    public List<Vector3> ButtonsPos2;
    //------------------------------------------------------
    [HideInInspector]
    public bool isFirstTimeEnter = true;
    //Settings
    [HideInInspector]
    public bool isMutedMusic;
    [HideInInspector]
    public bool isMutedSoundEffect;
    //-----
    [HideInInspector]
    public GameObject theScore;
    [HideInInspector]
    public GameObject theSave;
    [HideInInspector]
    public GameObject theSound;

    public bool beatAllLvl = false;

    // Start is called before the first frame update
    void Start()
    {
        theSave = GameObject.Find("saveLoadSystem");
        theScore = GameObject.Find("ScoreSystem");
        theSound = GameObject.Find("soundManager");

        Application.targetFrameRate = 60;
        if (theApp)
        {
            
        }
        else
        {
            theApp = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirstTimeEnter)
        {
            if(justEnterGamePanel)
            {
                justEnterGamePanel.SetActive(false);
            }
            if (lvlSelectPanel)
            {
                lvlSelectPanel.SetActive(true) ;
            }
        }
        isMutedMusic = !theSave.GetComponent<saveLoadSystem>().setting.MusicOnOff;
        isMutedSoundEffect = !theSave.GetComponent<saveLoadSystem>().setting.SoundEffectOnOff;


        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("N is pressed");
            SceneManager.LoadScene("GameEnter");
        }
        if(!backMenuButton && GameObject.Find("quitLvlButton"))
        {
            backMenuButton = GameObject.Find("quitLvlButton").GetComponent<Button>();
            backMenuButton.onClick.AddListener(OnBackMenuButtonClick);
            Debug.Log("quitButton Found");
        }
        if(!justEnterGamePanel && GameObject.Find("JustEnterGamePanel"))
        {
            justEnterGamePanel = GameObject.Find("JustEnterGamePanel");
            tapToStartButton = justEnterGamePanel.transform.GetComponent<Button>();
            tapToStartButton.onClick.AddListener(OnTapToStartButtonClick);
        }
        if(!pauseGamePanel && GameObject.Find("pauseMaskablePanel"))
        {
            pauseGamePanel = GameObject.Find("pauseMaskablePanel");
            pauseGamePanel.SetActive(false);

        }
        //pauseGamePanel = GameObject.Find("pauseMaskablePanel");
        if (!theRecentGame && GameObject.Find("Game"))
        {
            theRecentGame = GameObject.Find("Game");

        }
        if (!lvlSelectPanel && GameObject.Find("lvlSelectPanel"))
        {
            lvlSelectPanel = GameObject.Find("lvlSelectPanel");
            lvlSelectPanel.SetActive(false);
            SetLevelNameList();

        }
        if (!mainCamera && GameObject.Find("Main Camera"))
        {
            mainCamera = GameObject.Find("Main Camera");

        }
    }

    private void SetLevelNameList()
    {
        lvlName.Clear();
        for(int i = 0; i < lvlTotalNum; i++)
        {
            lvlName.Add(lvlSelectPanel.transform.GetChild(i).GetComponent<buttonEnterLvl>().sceneName);
        }
    }

    void OnBackMenuButtonClick()
    {
        Time.timeScale = 0;
        Debug.Log("quitButton clicked");
        pauseGamePanel.SetActive(true);

        if (GameObject.FindGameObjectWithTag("shootingPos").GetComponent<shooting>().isAiming)
        {
            GameObject.FindGameObjectWithTag("shootingPos").GetComponent<shooting>().isControllingAngle = false;
           
        }
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }


    void OnTapToStartButtonClick()
    {
        justEnterGamePanel.SetActive(false);
        mainCamera.GetComponent<storyVideo>().playStory();
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }
}
