using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRotation : MonoBehaviour
{
    public float rotationDegreesPerSec;
    GameObject rotationCenter;
    public bool isClockwise = false;
    [HideInInspector]
    public bool isOpen = false;
    float originalAngle;
    public float currentAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = transform.rotation.z;
        rotationCenter = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if(isClockwise)
        {
            currentAngle -= rotationDegreesPerSec * Time.deltaTime;
            rotationCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle ));
        }
        else
        {
            currentAngle += rotationDegreesPerSec * Time.deltaTime;
            rotationCenter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle ));
        }
    }
}
