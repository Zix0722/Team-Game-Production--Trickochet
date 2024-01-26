using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTranslation : MonoBehaviour
{
    GameObject movingBarrier;
    GameObject targetObj;
    Vector2 targetPos;
    Vector2 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        movingBarrier = transform.GetChild(1).gameObject;
        originalPos = movingBarrier.transform.position;
        targetObj = transform.GetChild(0).gameObject;
        targetPos = targetObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoTranslation()
    {
        if ((Vector2)movingBarrier.transform.position == targetPos)
        {
            movingBarrier.transform.position = originalPos;
            return;
        }
        if ((Vector2)movingBarrier.transform.position == originalPos)
        {
            movingBarrier.transform.position = targetPos;
            return;
        }

    }

    public void DoTranslationOnceOnly()
    {
        if ((Vector2)movingBarrier.transform.position == originalPos)
        {
            movingBarrier.transform.position = targetPos;
        }
    }
}
