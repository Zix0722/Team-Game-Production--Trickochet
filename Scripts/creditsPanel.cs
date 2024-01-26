using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsPanel : MonoBehaviour
{
    Button backButton;
    GameObject theApp;
    // Start is called before the first frame update
    void Start()
    {
        theApp = GameObject.Find("App");
        backButton = GameObject.Find("backButton").GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().QuitCredits();
    }
}
