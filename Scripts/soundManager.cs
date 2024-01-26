using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum soundEffect
{
    //------------------------sound effect--------------------
    starSpin,
    buttonClick,
    //--------------------------BGM----------------------------
    menuBGM,
    randomLevelBGM1,
    randomLevelBGM2,
    creditsBGM,
    randomLevelBGM3,
    randomLevelBGM4,
    randomLevelBGM5,
    //-------------------------------barrels
    barrelsMagic,
    barrelsDrum,
    //---------------------cactus
    cactusGetDmg
}

public class soundManager : MonoBehaviour
{
    public List<AudioClip> clipList;
    public GameObject AudioPlayerPrefab;
    public static GameObject theAudio;
    public GameObject theApp;
    public List<GameObject> audioPlayers;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayers.Capacity = 2;
        theApp = GameObject.Find("App");
        if (theAudio)
        {

        }
        else
        {
            theAudio = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        CreateBGMByManager(soundEffect.menuBGM);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var bgm in audioPlayers)
        {
            if(theApp.GetComponent<appSystem>().isMutedMusic)
            {
                if(bgm)
                {
                    bgm.GetComponent<AudioSource>().mute = true;
                }
            }
            else
            {
                if(bgm)
                {
                    bgm.GetComponent<AudioSource>().mute = false;
                }
            }
        }
        if (audioPlayers[0] == null)
        {
            audioPlayers[0] = GameObject.Find("audioPlayer(Clone)");
        }
    }

    public void PlaySoundByManager(soundEffect source)
    {
       
        if(theApp)
        {
            if(!theApp.GetComponent<appSystem>().isMutedSoundEffect)
            {
                var sound = GameObject.Instantiate(AudioPlayerPrefab, transform);
                sound.GetComponent<AudioSource>().clip = clipList[(int)source];
                sound.GetComponent<AudioSource>().Play();
            }
        }
    }
    public void CreateBGMByManager(soundEffect source)
    {
        if (theApp)
        {
           var sound = GameObject.Instantiate(AudioPlayerPrefab);
           audioPlayers.Add(sound);
           sound.GetComponent<AudioSource>().loop = true;
           sound.GetComponent<audioPlayer>().isBGM = true;
           sound.GetComponent<AudioSource>().clip = clipList[(int)source];
           sound.GetComponent<AudioSource>().Play();
           if(theApp.GetComponent<appSystem>().isMutedMusic)
            {
                sound.GetComponent<AudioSource>().mute = true;
            }
            
        }
    }

    public void EnterCredits()
    {
        audioPlayers[0].GetComponent<AudioSource>().Stop();
        CreateBGMByManager(soundEffect.creditsBGM);
    }

    public void QuitCredits()
    {
        Destroy(audioPlayers[1]);
        audioPlayers.RemoveAt(1);
        audioPlayers.TrimExcess();
        audioPlayers[0].GetComponent<AudioSource>().Play();

    }

    public void clearList()
    {
        audioPlayers.Clear();
    }

}
