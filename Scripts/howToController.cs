using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class howToController : MonoBehaviour
{

    Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton = transform.GetChild(1).GetComponent<Button>();
        closeButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClick()
    {
        gameObject.SetActive(false);
    }
}
