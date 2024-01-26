using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonEnterLvl : MonoBehaviour
{

    public GameObject loadingScreen;
    Button thisButton;
    public string sceneName;
    TextMeshProUGUI textMesh;
    public int lvlIndex;
    GameObject theApp;

    bool changedToUnlock = false;
    bool changedToBeaten = false;
    public bool unlock = false;
    public bool beaten = false;
    public Sprite locked_Sprite;
    public Sprite unlock_Sprite;
    public Sprite beaten_Sprite;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnlvlButtonClick);
        textMesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //textMesh.text = sceneName;
        theApp = GameObject.Find("App");

        gameObject.GetComponent<Image>().enabled = false;
        
    }

    private void OnEnable()
    {
        StartCoroutine(loadSaveData());
    }

    IEnumerator loadSaveData()
    {
        yield return new WaitForSeconds(0.001f);
        theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().SetLevelName(lvlIndex, sceneName);
        if (lvlIndex < 6)
        {
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().updateData(sceneName, saveLoadSystem.dataType.isUnlocked, true);
            Debug.Log("it");
        }
        if (lvlIndex == 18)
        {
            //theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().WriteData();
            Debug.Log("do it");
        }
        unlock = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().GetLevelIsUnlocked(lvlIndex);
        beaten = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().GetLevelIsBeanten(lvlIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(!changedToUnlock)
        {
            if(unlock)
            {
                GetComponent<Image>().sprite = unlock_Sprite;
                transform.GetChild(0).gameObject.SetActive(true);
                changedToUnlock = true;
                gameObject.GetComponent<Image>().enabled = true;
            }
        }
        beaten = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().GetLevelIsBeanten(lvlIndex);
        //if (!changedToBeaten)
        {
            if (beaten)
            {
                GetComponent<Image>().sprite = beaten_Sprite;
                changedToBeaten = true;
                gameObject.GetComponent<Image>().enabled = true;
            }
        }
        gameObject.GetComponent<Image>().enabled = true;
    }

    void OnlvlButtonClick()
    {
        if (!unlock)
        {
            return;
        }
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        if (sceneName != null)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(waitSecToGetIn());
        }
        theApp.GetComponent<appSystem>().isFirstTimeEnter = false;
        if(lvlIndex > 11)
        {
            theApp.GetComponent<appSystem>().currentPageForLevelSelect = 2;
        }
        else
        {
            theApp.GetComponent<appSystem>().currentPageForLevelSelect = 1;
        }
    }

    IEnumerator waitSecToGetIn()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
