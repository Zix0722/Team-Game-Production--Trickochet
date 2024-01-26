using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitWindow : MonoBehaviour
{

    GameObject maskablePanel;
    Button yesButton;
    Button noButton;
    GameObject theApp;
    public bool isClearFile = false;
    // Start is called before the first frame update
    void Start()
    {
        theApp = GameObject.Find("App");
        maskablePanel = transform.parent.gameObject;
        yesButton = transform.GetChild(1).GetComponent<Button>();
        noButton = transform.GetChild(2).GetComponent<Button>();

        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnYesButtonClick()
    {
        if(!isClearFile)
        {
            Application.Quit();
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        }
        else
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
            maskablePanel.gameObject.SetActive(false);
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().clearData();
        }
    }

    void OnNoButtonClick()
    {
        maskablePanel.gameObject.SetActive(false);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);

    }
}
