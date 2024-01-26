using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameCommon : MonoBehaviour
{
    public bool isThereAmmoInAir = false;
    public int numAmmo = 6;
    public int numPickedStar = 0;

    public GameObject outOfAmmoWIN;
    public List<GameObject> enermiesList;
    public int enemiesNum;
    public GameObject LvlWinWindow;
    public GameObject howToWIN;
    public bool showHowTo = false;
    [HideInInspector]
    public bool isEmpty = false;
    [HideInInspector]
    public bool isWin = false;
    [HideInInspector]
    public bool cannotControl = false;
    public int levelIndex;
    [HideInInspector]
    public GameObject theApp;
    public string nextLvlName;
    public string previousLvlName;
    //[HideInInspector]
    public string lvlName;
    List<string> nameList;

    public GameObject HUD;
    public GameObject BGM;

    bool havePlayed = false;
    bool haveUpdated = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60;
        theApp = GameObject.Find("App");
        float seed = Random.Range(0f, 5f);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().clearList();
        if (seed >= 0f && seed <= 1f)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().CreateBGMByManager(soundEffect.randomLevelBGM1);
        }
        else if(seed >= 1f && seed <= 2f)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().CreateBGMByManager(soundEffect.randomLevelBGM2);
        }
        else if (seed >= 2f && seed <= 3f)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().CreateBGMByManager(soundEffect.randomLevelBGM3);
        }
        else if (seed >= 3f && seed <= 4f)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().CreateBGMByManager(soundEffect.randomLevelBGM4);
        }
        else if (seed >= 4f && seed <= 5f)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().CreateBGMByManager(soundEffect.randomLevelBGM5);
        }

    }
    void Start()
    {
        enemiesNum = enermiesList.Count;
        if(showHowTo)
        {
            howToWIN.SetActive(true);
        }
        if(theApp)
        {
            nameList = theApp.GetComponent<appSystem>().lvlName;
            Scene scene = SceneManager.GetActiveScene();
            var currentSceneName = scene.name;
            for(int i = 0; i < nameList.Count; i++)
            {
                if(nameList[i] == currentSceneName)
                {
                    if(i == nameList.Count - 1)
                    {
                        nextLvlName = null;
                        lvlName = nameList[i];
                        levelIndex = i;
                        previousLvlName = nameList[i - 1];
                        break;
                    }
                    lvlName = nameList[i];
                    levelIndex = i;
                    nextLvlName = nameList[i + 1];
                    if (i == 0)
                    {
                        previousLvlName = null;
                       
                    }
                    else
                    {
                        previousLvlName = nameList[i - 1];
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        checkWin();
        if (isWin)
        {
            
            StartCoroutine(showVectoryWindowAndBack());
            return;
        }
        markEmpty();
        if (isEmpty && !isThereAmmoInAir && !isWin)
        {
            StartCoroutine(showWindowAndReset());
        }
    }

    void markEmpty()
    {
        if (numAmmo <= 0)
        {
            isEmpty = true;
        }
    }

    IEnumerator showWindowAndReset()
    {
        yield return new WaitForSeconds(3.0f);
        outOfAmmoWIN.SetActive(true);
        outOfAmmoWIN.GetComponent<winLoseScreen>().isWinWindow = false;
    }


    IEnumerator showVectoryWindowAndBack()
    {
        
        cannotControl = true;
        if (numAmmo == 5 && !havePlayed && numPickedStar == 3)
        {
            HUD.GetComponent<HUDcontroller>().playTrickochet();
            havePlayed = true;
        }
        yield return new WaitForSeconds(3.0f);
        LvlWinWindow.SetActive(true);
        if(!haveUpdated)
        {
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().updateData(lvlName, saveLoadSystem.dataType.isBeaten, true);
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().updateData(nextLvlName, saveLoadSystem.dataType.isUnlocked, true);
            haveUpdated = true;
        }
        //-----------------return ---------------------
        
        
    }

    void checkWin()
    {
        
        if (enermiesList.Count == 0)
        {
            isWin = true;
        }
    }

    public void goNextlvl()
    {
        if (nextLvlName != null)
        {
            SceneManager.LoadScene(nextLvlName);
            theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();

        }
    }

    public void goPreviouslvl()
    {
        if (previousLvlName != null)
        {
            SceneManager.LoadScene(previousLvlName);
            theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();

        }
    }
}
