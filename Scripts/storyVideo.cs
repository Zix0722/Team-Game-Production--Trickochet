using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storyVideo : MonoBehaviour
{
    VideoPlayer vp;
    public VideoClip intro;
    public VideoClip outro;
    public GameObject mainCanvas;
    public GameObject videoCanvas;
    public bool beatAllLevels = false;
    public bool havePlayed = false;
    GameObject theApp;
    Button skipButton;
    // Start is called before the first frame update
    void Start()
    {
        theApp = GameObject.Find("App");
        vp = GetComponent<VideoPlayer>();
        skipButton = videoCanvas.transform.GetChild(0).GetComponent<Button>();
        skipButton.onClick.AddListener(OnButtonSkipClick);
    }

    // Update is called once per frame
    void Update()
    {
        beatAllLevels = theApp.GetComponent<appSystem>().beatAllLvl;
        if(beatAllLevels && !havePlayed)
        {
            playOntroVideo();
            havePlayed = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("GameEnter"); 
        }
    }

    public void playStory()
    {
        StartCoroutine(playStoryAndGoBack());
    }
    IEnumerator playStoryAndGoBack()
    {
        mainCanvas.SetActive(false);
        vp.clip = intro;
        vp.Play();
        yield return new WaitForSeconds(23f);
        vp.Stop();
        mainCanvas.SetActive(true);
    }

    public void OnButtonSkipClick()
    {
        mainCanvas.SetActive(true);
        vp.Stop();
    }

    public void playOntroVideo()
    {
        StartCoroutine(playFinalStoryAndGoBack());
    }

    IEnumerator playFinalStoryAndGoBack()
    {
        mainCanvas.SetActive(false);
        vp.clip = outro;
        vp.Play();
        yield return new WaitForSeconds(14f);
        vp.Stop();
        mainCanvas.SetActive(true);
    }
}
