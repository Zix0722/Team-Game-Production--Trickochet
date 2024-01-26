using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDcontroller : MonoBehaviour
{
    public GameObject enemyCount;
    public GameObject starCount;
    public GameObject sanchoReact;
    public GameObject theGame;

    public int remainEnemyNum = 0;
    public int killedEnemyNum = 0;

    List<GameObject> stars;
    List<GameObject> bottles;
    List<GameObject> allMoods;

    Animator anim;
    int starsCounter = 0;
    public sanchoMood currentMood = sanchoMood.happy;
    // Start is called before the first frame update
    void Start()
    {
        remainEnemyNum = theGame.GetComponent<gameCommon>().enemiesNum;
        stars = new List<GameObject>();
        bottles = new List<GameObject>();
        allMoods = new List<GameObject>();

        anim = sanchoReact.GetComponent<Animator>();
        // all mood

        allMoods.Add(sanchoReact.transform.GetChild(0).gameObject);
        allMoods.Add(sanchoReact.transform.GetChild(1).gameObject);
        allMoods.Add(sanchoReact.transform.GetChild(2).gameObject);
        allMoods.Add(sanchoReact.transform.GetChild(3).gameObject);
        allMoods.Add(sanchoReact.transform.GetChild(4).gameObject);

        // all stars
        stars.Add(starCount.transform.GetChild(0).gameObject);
        stars.Add(starCount.transform.GetChild(1).gameObject);
        stars.Add(starCount.transform.GetChild(2).gameObject);

        // all bottles 
        bottles.Add(enemyCount.transform.GetChild(0).gameObject);
        bottles.Add(enemyCount.transform.GetChild(1).gameObject);
        bottles.Add(enemyCount.transform.GetChild(2).gameObject);
        bottles.Add(enemyCount.transform.GetChild(3).gameObject);
        bottles.Add(enemyCount.transform.GetChild(4).gameObject);
        bottles.Add(enemyCount.transform.GetChild(5).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum sanchoMood
    {
        angry,
        blinking,
        happy,
        sad,
        shocked,
        none
    }

    public void playSad()
    {
        anim.SetTrigger("Sad");
    }

    public void playHappy()
    {
        anim.SetTrigger("Happy");
    }

    public void playBlinking()
    {
        anim.SetTrigger("Blinking");
    }

    public void playAngry()
    {
        anim.SetTrigger("Angry");
    }

    public void playShocked()
    {
        anim.SetTrigger("Shocked");
    }
    public void playTrickochet()
    {
        anim.SetTrigger("Trickochet");
    }

    public void getStar()
    {
        starsCounter++;
        if(starsCounter == 1)
        {
            stars[0].GetComponent<Animator>().SetTrigger("getStar");
        }
        if (starsCounter == 2)
        {
            stars[1].GetComponent<Animator>().SetTrigger("getStar");
        }
        if (starsCounter == 3)
        {
            stars[2].GetComponent<Animator>().SetTrigger("getStar");
        }

    }

    public void killEnemy()
    {
        remainEnemyNum--;
        killedEnemyNum++;
        bottles[killedEnemyNum - 1].GetComponent<Animator>().SetInteger("enemyKilled", killedEnemyNum);
    }

}
