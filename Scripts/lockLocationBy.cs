using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockLocationBy : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if(Target)
        {
            Target.GetComponent<shooting>().isLocked = false;
        }
       
    }


}
