using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class guildhallSplashVideo : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(delayLoadGameEnterScene());
    }
    void SplashVideo()
    {

    }

    IEnumerator delayLoadGameEnterScene()
    {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("teamLogo");

    }
}
