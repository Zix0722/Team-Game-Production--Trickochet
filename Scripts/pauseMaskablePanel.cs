using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseMaskablePanel : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject resetButton;
    public GameObject mainMenuButton;
    public GameObject optionsButton;
    public GameObject exitGameButton;
    public GameObject askingExitWindow;
    public GameObject optionPanel;
    GameObject theApp;
    // Start is called before the first frame update
    void Start()
    {
        theApp = GameObject.Find("App");
        resumeButton.GetComponent<Button>().onClick.AddListener(OnResumeButtonClick);
        resetButton.GetComponent<Button>().onClick.AddListener(OnResetButtonClick);
        mainMenuButton.GetComponent<Button>().onClick.AddListener(OnMainMenuButtonClick);
        optionsButton.GetComponent<Button>().onClick.AddListener(OnOptionButtonClick);
        exitGameButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnResumeButtonClick()
    {
        gameObject.SetActive(false);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        Time.timeScale = 1;

        if(GameObject.Find("shootingObj"))
        {
            if (GameObject.Find("shootingObj").GetComponent<shooting>().isAiming)
            {
                GameObject.Find("shootingObj").GetComponent<shooting>().isControllingAngle = true;
            }
        }
        if(GameObject.Find("shootingObj Variant"))
        {
            if (GameObject.Find("shootingObj Variant").GetComponent<shooting>().isAiming)
            {
                GameObject.Find("shootingObj Variant").GetComponent<shooting>().isControllingAngle = true;
            }
        }
    }

    void OnResetButtonClick()
    {
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (theApp)
        {
            theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();
        }
        Time.timeScale = 1;
    }

    void OnMainMenuButtonClick()
    {
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        Time.timeScale = 1;
        theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();
        SceneManager.LoadScene("GameEnter");
    }

    void OnOptionButtonClick()
    {
        optionPanel.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnExitButtonClick()
    {
        askingExitWindow.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

}
