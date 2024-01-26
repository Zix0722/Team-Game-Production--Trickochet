using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static GameObject theScore;
    public int ricochetPoint = 25;
    public int enemyKillPoint = 100;
    public int starPickUpPoint = 200;
    public float eachRemainAmmoFactor = 2f;

    public GameObject ScoreUI;
    public GameObject popUpScorePrefab;
    public int totalBaseScore;
    public int finalScore;

    GameObject theGame;
    float randAngle;
    // Start is called before the first frame update
    void Start()
    {
       
        if (theScore)
        {

        }
        else
        {
            theScore = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // getFinalScore();
       if(!theGame && GameObject.Find("Game"))
        {
            theGame = GameObject.Find("Game");
        }
    }

    [System.Obsolete]
    public void Ricochet(Transform transform)
    {
        if(theGame.GetComponent<gameCommon>().isWin)
        {
            return;
        }
        randAngle = Random.RandomRange(-20f, 20f);
        GameObject thePopup;
        thePopup = GameObject.Instantiate(popUpScorePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, randAngle)));
        thePopup.GetComponent<TextMesh>().text = "+" + ricochetPoint;
        thePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + ricochetPoint;
        totalBaseScore += ricochetPoint;
        Destroy(thePopup, 0.5f);
    }
    [System.Obsolete]
    public void killAEnemy(Transform transform)
    {
        if (theGame.GetComponent<gameCommon>().isWin)
        {
            return;
        }
        randAngle = Random.RandomRange(-20f, 20f);
        GameObject thePopup;
        thePopup = GameObject.Instantiate(popUpScorePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, randAngle))) as GameObject;
        thePopup.GetComponent<TextMesh>().text = "+" + enemyKillPoint;
        thePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + enemyKillPoint;
        totalBaseScore += enemyKillPoint;
        Destroy(thePopup, 1f);
    }
    [System.Obsolete]
    public void pickUpStar(Transform transform)
    {
        if (theGame.GetComponent<gameCommon>().isWin)
        {
            return;
        }
        randAngle = Random.RandomRange(-20f, 20f);
        GameObject thePopup;
        thePopup = GameObject.Instantiate(popUpScorePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, randAngle))) as GameObject;
        thePopup.GetComponent<TextMesh>().text = "+" + starPickUpPoint;
        thePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + starPickUpPoint;
        totalBaseScore += starPickUpPoint;
        Destroy(thePopup, 1.5f);
    }

    public void renewScores()
    {
        totalBaseScore = 0;
        finalScore = 0;
    }    

    public int getFinalScore()
    {
        theGame = GameObject.Find("Game");
        finalScore = totalBaseScore * (theGame.GetComponent<gameCommon>().numAmmo + 1);
        return finalScore;
    }
}
