using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireButton : MonoBehaviour
{
    public Button shootingButton;
    public GameObject currentShootingObj;
    GameObject shootingObj;
    GameObject theGame;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //shootingObj = GameObject.Find("shootingObj Variant");
        theGame = GameObject.Find("Game");
        shootingButton = GetComponent<Button>();
        anim = GetComponent<Animator>();
        shootingButton.onClick.AddListener(OnShootingButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnShootingButtonClick()
    {
        if (!theGame.GetComponent<gameCommon>().cannotControl)
        {
            currentShootingObj.GetComponent<shooting>().doFire(currentShootingObj.GetComponent<shooting>().dir);
        }
        anim.SetTrigger("touch");
    }
}
