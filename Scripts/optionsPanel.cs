using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsPanel : MonoBehaviour
{

    public GameObject askPanel;
    Button menuButton;
    Button credits;
    GameObject volume;
    GameObject music;
    Button volumeButton;
    Button musicButton;
    Button introButton;
    Button outroButton;
    Button clearProgress;

    public GameObject creditsPanel;

    GameObject theApp;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite soundEffectOn;
    public Sprite soundEffectOff;

    GameObject mainCam;
    // Start is called before the first frame update

    private void OnEnable()
    {
        creditsPanel.SetActive(false);
       
    }

   
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");

        menuButton = GameObject.Find("backMenu").GetComponent<Button>();
        menuButton.onClick.AddListener(OnMenuButtonClick);

        credits = GameObject.Find("credits").GetComponent<Button>();
        credits.onClick.AddListener(OnCreditsButtonClick);

        volume = GameObject.Find("sfx");
        volumeButton = volume.GetComponent<Button>();
        volumeButton.onClick.AddListener(OnVolumeButtonClick);

        music = GameObject.Find("music");
        musicButton = music.GetComponent<Button>();
        musicButton.onClick.AddListener(OnMusicButtonClick);

        introButton = GameObject.Find("intro").GetComponent<Button>();
        introButton.onClick.AddListener(OnIntroButtonClick);
        outroButton = GameObject.Find("outro").GetComponent<Button>();
        outroButton.onClick.AddListener(OnOutroButtonClick);

        clearProgress = GameObject.Find("reset progress").GetComponent<Button>();
        clearProgress.onClick.AddListener(OnClearButtonClick);

    }

    // Update is called once per frame
    void Update()
    {
        if (!theApp)
        {
            theApp = GameObject.Find("App");
            if (theApp)
            {

                if (!theApp.GetComponent<appSystem>().isMutedMusic)
                {
                    music.GetComponent<Image>().sprite = musicOn;

                }
                else
                {
                    music.GetComponent<Image>().sprite = musicOff;

                }
                if (!theApp.GetComponent<appSystem>().isMutedSoundEffect)
                {
                    volume.GetComponent<Image>().sprite = soundEffectOn;
                }
                else
                {
                    volume.GetComponent<Image>().sprite = soundEffectOff;
                }

            }
            if(!introButton && GameObject.Find("intro").GetComponent<Button>())
            {
                introButton = GameObject.Find("intro").GetComponent<Button>();
                introButton.onClick.AddListener(OnIntroButtonClick);
            }
            if(!outroButton && GameObject.Find("outro").GetComponent<Button>())
            {
                outroButton = GameObject.Find("outro").GetComponent<Button>();
                outroButton.onClick.AddListener(OnOutroButtonClick);
            }
        }
    }

    void OnMenuButtonClick()
    {
        gameObject.SetActive(false);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnCreditsButtonClick()
    {
        creditsPanel.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().EnterCredits();
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnMusicButtonClick()
    {
        if (theApp)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.MusicOnOff = !theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.MusicOnOff;
            bool currentSettingMusic = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.MusicOnOff;
            bool currentSettingEffect = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.SoundEffectOnOff;
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().WriteSetting(currentSettingMusic, currentSettingEffect);
            if(currentSettingMusic == true)
            {
                music.GetComponent<Image>().sprite = musicOn;
                //music.GetComponent<Image>().color = Color.white;
            }
            else
            {
                music.GetComponent<Image>().sprite = musicOff;
                //music.GetComponent<Image>().color = Color.red;
            }
        }
    }

    void OnVolumeButtonClick()
    {
        if (theApp)
        {
            theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.SoundEffectOnOff = !theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.SoundEffectOnOff;
            bool currentSettingMusic = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.MusicOnOff;
            bool currentSettingEffect = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().setting.SoundEffectOnOff;
            theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>().WriteSetting(currentSettingMusic, currentSettingEffect);

            if (currentSettingEffect == true)
            {
                volume.GetComponent<Image>().sprite = soundEffectOn;
                //volume.GetComponent<Image>().color = Color.white;
            }
            else
            {
                volume.GetComponent<Image>().sprite = soundEffectOff;
                //volume.GetComponent<Image>().color = Color.red;
            }
        }
    }

    void OnIntroButtonClick()
    {
        mainCam.GetComponent<storyVideo>().playStory();
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }

    void OnOutroButtonClick()
    {
        mainCam.GetComponent<storyVideo>().playOntroVideo();
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }
    void OnClearButtonClick()
    {
        askPanel.SetActive(true);
        theApp.GetComponent<appSystem>().theSound.GetComponent<soundManager>().PlaySoundByManager(soundEffect.buttonClick);
    }
}

