using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScore : MonoBehaviour
{
    public int increaseRate = 5;
    public bool isFinalWindow = false;
    public bool endAnim = false;

    TextMeshProUGUI text1;
    TextMeshProUGUI text2;
    TextMeshProUGUI text3;
    TextMeshProUGUI text4;

    int goalScore = 0;
    int currentScore = 0;

    GameObject theApp;
    GameObject theGame;
    // Start is called before the first frame update
    void Start()
    {
        theApp = GameObject.Find("App");
        theGame = GameObject.Find("Game");
        text1 = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text3 = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        text4 = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFinalWindow)
        {
            if(endAnim)
            {
                currentScore = goalScore;
            }
            if (theApp)
            {
                goalScore = theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().getFinalScore();
            }
            if (currentScore < goalScore)
            {
                if ((currentScore + increaseRate) > goalScore)
                {
                    currentScore = goalScore;
                }
                else
                {
                    currentScore += increaseRate;
                }
            }
        }
        else
        {
            if(theGame.GetComponent<gameCommon>().cannotControl)
            {
                text1.text = goalScore.ToString();
                text2.text = goalScore.ToString();
                text3.text = goalScore.ToString();
                text4.text = goalScore.ToString();
                return;
            }
            if(theApp)
            {
                goalScore = theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().totalBaseScore;
            }

            if(currentScore < goalScore)
            {
                if((currentScore + increaseRate) > goalScore)
                {
                    currentScore = goalScore;
                }
                else
                {
                    currentScore += increaseRate;
                }
            }
        }

        text1.text = currentScore.ToString();
        text2.text = currentScore.ToString();
        text3.text = currentScore.ToString();
        text4.text = currentScore.ToString();
    }
}
