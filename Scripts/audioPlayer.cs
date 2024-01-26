using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isBGM = false;
    void Start()
    {
        if(isBGM)
        {

        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
