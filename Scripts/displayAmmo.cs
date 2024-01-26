using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayAmmo : MonoBehaviour
{
    public List<GameObject> ammoIconList;
    GameObject theCylinder;
    GameObject theGame;
    Vector3 localScale;
    float angle = 0;
    void Start()
    {
        theGame = GameObject.Find("Game");
        theCylinder = gameObject;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        theCylinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        refreshIcons();
        var numAmmo = theGame.GetComponent<gameCommon>().numAmmo;
        for(int iconIndex = 0; iconIndex < numAmmo; iconIndex ++)
        {
            ammoIconList[iconIndex].SetActive(true);
        }
    }

    void refreshIcons()
    {
        foreach(var icon in ammoIconList)
        {
            icon.SetActive(false);
        }
    }
    public IEnumerator ReloadAmmo()
    {
        for(int i = 0; i < 18; i++ )
        {
            angle = i * 20;
            yield return new WaitForEndOfFrame();
        }
       
        angle = 0;
    }
}
