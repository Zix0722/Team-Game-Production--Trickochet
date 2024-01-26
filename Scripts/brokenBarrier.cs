using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenBarrier : MonoBehaviour
{
    public bool isBroken = false;
    public int HP = 3;

    int initialHP;
    float currentPercentage;
    public Sprite unDamaged;
    public Sprite intactBrick;
    public Sprite crackedBrick;
    

    // Start is called before the first frame update
    void Start()
    {
        initialHP = HP;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        

        if (HP <= 0)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isBroken)
        {
            return;
        }
        if (collision.gameObject.CompareTag("projectile"))
        {
            SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
            HP -= 1;
            currentPercentage = (float)HP  / (float)initialHP;
//             Debug.Log(HP);
//             Debug.Log(initialHP);
//             Debug.Log(currentPercentage);
            float red = RangeMapClamped(currentPercentage, 0.333f, 0.666f, 1f, 0f);
            float green = RangeMapClamped(currentPercentage, 0f, 0.333f, 0f, 1f);
            float blue = RangeMapClamped(currentPercentage, 0.666f, 1f, 0f, 1f);
            if ((float)HP <= initialHP * 0.5f)
            {
                //Sprite sprite = Resources.Load("Textrues/XXX", typeof(Sprite)) as Sprite;
                sr.sprite = intactBrick;
            }

            if(HP <= 0)
            {

                //Sprite sprite = Resources.Load("Textrues/XXX", typeof(Sprite)) as Sprite;
                GetComponent<PolygonCollider2D>().enabled = false;
                sr.sprite = crackedBrick;
            }
            //do animations
            sr.color = new Color(255 * red, 255 * green, 255 * blue);


        }
    }

    float GetFractionWithinRange(float value, float rangeStart, float rangeEnd)
    {
        float range = rangeEnd - rangeStart;
        float valFromRange = value - rangeStart;

        return valFromRange / range;
    }

    float RangeMapClamped(float inValue, float inStart, float inEnd, float outStart, float outEnd)
    {
        if (inValue < inStart)
        {
            return outStart;
        }
        if (inValue > inEnd)
        {
            return outEnd;
        }
        // do same as rangeMap
        float fraction = GetFractionWithinRange(inValue, inStart, inEnd);
        float outRange = outEnd - outStart;
        float result = outRange * fraction;
        result += outStart;
        return result;

    }
}
