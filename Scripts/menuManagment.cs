using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManagment : MonoBehaviour
{

    public Button playGameButton;
    public Button optionsButton;
    public Button quit;
    public GameObject askingExitWindowPrefab;
    public GameObject lvlSelectPanel;
    public GameObject optionsPanel;
    GameObject theApp;
    // Start is called before the first frame update
    void Start()
    {
        playGameButton.onClick.AddListener(OnplayGameButtonClick);
        optionsButton.onClick.AddListener(OnoptionsButtonClick);
        quit.onClick.AddListener(OnQuitButtonClick);
        //lvlSelectPanel.SetActive(false);
        optionsPanel.SetActive(false);
        theApp = GameObject.Find("App");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnplayGameButtonClick()
    {
        lvlSelectPanel.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnoptionsButtonClick()
    {
        optionsPanel.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnQuitButtonClick()
    {
        askingExitWindowPrefab.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }
}
