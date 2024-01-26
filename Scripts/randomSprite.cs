using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomSprite : MonoBehaviour
{
    public List<Sprite> sprites;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        float seed = Random.Range(0f, 3f);
        //Debug.Log(seed);
        if(seed >= 0 && seed < 1)
        {
            image.sprite = sprites[0];
        }
        else if(seed >= 1 && seed < 2)
        {
            image.sprite = sprites[1];
        }
        else
        {
            image.sprite = sprites[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
