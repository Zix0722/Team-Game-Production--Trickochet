using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class countDown : MonoBehaviour
{
    public int countFrom = 90;
    public GameObject caseWin;
    TextMeshProUGUI t;
    IEnumerator coroutine;

    GameObject theGame;
    // Start is called before the first frame update
    void Start()
    {
        theGame = GameObject.Find("Game");

        t = GetComponent<TextMeshProUGUI>();
        t.enabled = true;
        coroutine = StartCountDown();
        StartCoroutine(coroutine);
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    

    IEnumerator StartCountDown()
    {
        t.text = countFrom.ToString();
        for (int i = countFrom; i >= 0; i--)
        {
            if(theGame.GetComponent<gameCommon>().isWin)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
            t.text = i.ToString();
            if (i == 0)
            {
                caseWin.SetActive(true);
                theGame.GetComponent<gameCommon>().cannotControl = true;
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // may be removed;
                yield break;
            }
        }
    }
}
