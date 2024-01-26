using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class teamLogo : MonoBehaviour
{
    public  AsyncOperation async = null;
    private void Awake()
    {
       
    }
    void Start()
    {
        async = SceneManager.LoadSceneAsync("GameEnter");
        async.allowSceneActivation = false;
        StartCoroutine(delayLoadGameEnterScene());
    }
    void SplashVideo()
    {

    }

    IEnumerator delayLoadGameEnterScene()
    {
        yield return new WaitForSeconds(3f);
        async.allowSceneActivation = true;
    }
}
