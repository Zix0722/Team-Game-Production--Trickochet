using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationAndStay : MonoBehaviour
{
    public float rotationDegrees;
    GameObject rotationCenter;
    [HideInInspector]
    public bool isOpen = false;
    float originalAngle;
    float timeCounter;
    public float staySeconds = 2f;
    bool isOriginal = true;
    // Start is called before the first frame update
    void Start()
    {
        originalAngle = transform.rotation.z;
        rotationCenter = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= staySeconds && isOriginal)
        {
            rotationCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, originalAngle + rotationDegrees));
            timeCounter = 0;
            isOriginal = false;
        }
        else if(timeCounter >= staySeconds && !isOriginal)
        {
            rotationCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, originalAngle));
            timeCounter = 0;
            isOriginal = true;
        }
    }
}
