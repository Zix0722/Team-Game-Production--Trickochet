using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheildCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<guardEnermy>().isTouchSheild = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<guardEnermy>().isTouchSheild = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        transform.parent.GetComponent<guardEnermy>().isTouchSheild = true;
    }
}
