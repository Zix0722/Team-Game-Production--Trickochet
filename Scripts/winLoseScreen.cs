using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winLoseScreen : MonoBehaviour
{
    public bool isWinWindow = true;
    //---------------win
    public Button tryAgainButton;
    public Button goNextButton;
    public Button typeToSkipAnimButton;
    //--------------lose
    public Button goBackMenu;
    //----------------------
    public GameObject RollingScore;
    GameObject theApp;
    GameObject theGame;
    public GameObject firstRankScore;
    public GameObject secRankScore;
    public GameObject thirdRankScore;
    public GameObject theNewSymbol;
    public GameObject theNewSymbol2;
    public GameObject theNewSymbol3;
    public GameObject multiplyer;

    TextMeshProUGUI text1;
    TextMeshProUGUI text2;
    TextMeshProUGUI text3;
    TextMeshProUGUI mutiplyerText;
    public TMP_FontAsset fontWhite;
    public TMP_FontAsset fontPink;


    int firstScore = 0;
    int secScore = 0;
    int thirdScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(theNewSymbol)
        {
            theNewSymbol.SetActive(false);
            theNewSymbol2.SetActive(false);
            theNewSymbol3.SetActive(false);
            
        }
        theApp = GameObject.Find("App");
        theGame = GameObject.Find("Game");
        if (firstRankScore)
        {
            text1 = firstRankScore.GetComponent<TextMeshProUGUI>();
            text2 = secRankScore.GetComponent<TextMeshProUGUI>();
            text3 = thirdRankScore.GetComponent<TextMeshProUGUI>();
            mutiplyerText = multiplyer.GetComponent<TextMeshProUGUI>();
            mutiplyerText.text = (theGame.GetComponent<gameCommon>().numAmmo + 1).ToString();

            text1.font = fontWhite;
            text2.font = fontWhite;
            text3.font = fontWhite;

            var saveScipt = theApp.GetComponent<appSystem>().theSave.GetComponent<saveLoadSystem>();
            int index = theGame.GetComponent<gameCommon>().levelIndex;
            firstScore = saveScipt.saveLvlScores[index].HighestScore;
            secScore = saveScipt.saveLvlScores[index].SecondScore;
            thirdScore = saveScipt.saveLvlScores[index].ThirdtScore;

            int final = theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().getFinalScore();

            if (final >= firstScore)
            {
                thirdScore = secScore;
                secScore = firstScore;
                firstScore = final;
                theNewSymbol.SetActive(true);
                text1.font = fontPink;
                
            }
            else if (final >= secScore && final < firstScore)
            {
                thirdScore = secScore;
                secScore = final;
                theNewSymbol2.SetActive(true);
                text2.font = fontPink;
            }
            else if (final >= thirdScore && final < secScore)
            {
                thirdScore = final;
                theNewSymbol3.SetActive(true);
                text3.font = fontPink;
            }
            if(text1)
            {
                text1.text = firstScore.ToString();
                text2.text = secScore.ToString();
                text3.text = thirdScore.ToString();
            }

            string lvlname = theGame.GetComponent<gameCommon>().lvlName;
            saveScipt.updateData(lvlname, saveLoadSystem.dataType.HighestScore, firstScore);
            saveScipt.updateData(lvlname, saveLoadSystem.dataType.SecondScore, secScore);
            saveScipt.updateData(lvlname, saveLoadSystem.dataType.ThirdtScore, thirdScore);
        }

        tryAgainButton.onClick.AddListener(OntryAgainButtonClick);
        goNextButton.onClick.AddListener(OngoNextButtonClick);
        typeToSkipAnimButton.onClick.AddListener(OntypeToSkipAnimButtonClick);
        goBackMenu.onClick.AddListener(OngoBackMenuClick);

        var gameScript = theGame.GetComponent<gameCommon>();
        if(isWinWindow)
        {
            goBackMenu.gameObject.SetActive(false);
            goNextButton.gameObject.SetActive(true);
        }
        else
        {
            goBackMenu.gameObject.SetActive(true);
            goNextButton.gameObject.SetActive(false) ;
        }

        if (gameScript.nextLvlName == "back")
        {
            goNextButton.gameObject.SetActive(false);
            goBackMenu.gameObject.SetActive(true);
        }
        else
        {
            if (gameScript.nextLvlName != null)
            {
                

            }
            else
            {
                goNextButton.gameObject.SetActive(false);
                goBackMenu.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OntryAgainButtonClick()
    {
        var gameScript = theGame.GetComponent<gameCommon>();
        SceneManager.LoadScene(gameScript.lvlName);
        theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();
    }

    void OngoNextButtonClick()
    {
        var gameScript = theGame.GetComponent<gameCommon>();
        SceneManager.LoadScene(gameScript.nextLvlName);
        theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();
    }

    void OntypeToSkipAnimButtonClick()
    {
        RollingScore.GetComponent<HUDScore>().endAnim = true;
    }

    void OngoBackMenuClick()
    {
        var gameScript = theGame.GetComponent<gameCommon>();
        if (gameScript.levelIndex == 18)
        {
            theApp.GetComponent<appSystem>().beatAllLvl = true;
        }

        SceneManager.LoadScene("GameEnter");
        theApp.GetComponent<appSystem>().theScore.GetComponent<ScoreSystem>().renewScores();
    }
}
