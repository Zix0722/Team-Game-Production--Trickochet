using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public GameObject linkedBarrier;
    public bool canTurnOff;
    public float AnimSeconds = 0.3f;
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("touched");
        if(collision.gameObject.CompareTag("projectile"))
        {
            StartCoroutine(playAnim());
            if(canTurnOff)
            {
                if(linkedBarrier.gameObject.GetComponent<entityMoving>())
                {
                    linkedBarrier.gameObject.GetComponent<entityMoving>().isMoving = !linkedBarrier.gameObject.GetComponent<entityMoving>().isMoving;   
                }
                if(linkedBarrier.gameObject.GetComponent<ToggleTranslation>())
                {
                    linkedBarrier.gameObject.GetComponent<ToggleTranslation>().DoTranslation();
                }
                if (linkedBarrier.gameObject.GetComponent<ToggleRotation>())
                {
                    linkedBarrier.gameObject.GetComponent<ToggleRotation>().isOpen = !linkedBarrier.gameObject.GetComponent<ToggleRotation>().isOpen;
                }
            }
            else
            {
                if (linkedBarrier.gameObject.GetComponent<entityMoving>())
                {
                    linkedBarrier.gameObject.GetComponent<entityMoving>().isMoving = true;
                    Debug.Log(linkedBarrier.gameObject.GetComponent<entityMoving>().isMoving);
                }
                if (linkedBarrier.gameObject.GetComponent<ToggleTranslation>())
                {
                    linkedBarrier.gameObject.GetComponent<ToggleTranslation>().DoTranslationOnceOnly();
                }
                if (linkedBarrier.gameObject.GetComponent<ToggleRotation>())
                {
                    linkedBarrier.gameObject.GetComponent<ToggleRotation>().isOpen = true;
                }
            }
            
        }

    }

    IEnumerator playAnim()
    {
        anim.SetBool("play", true);
        Debug.Log("play anim");
        yield return new WaitForSeconds(AnimSeconds);
        anim.SetBool("play", false);
    }
}
