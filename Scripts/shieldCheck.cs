using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCheck : MonoBehaviour
{
    public bool frontShield = false;
    public bool backShield = false;
    public bool leftShield = false;
    public bool rightShield = false;

    Vector3 enermyPos;

    Vector3 frontDirection;
    Vector3 backDirection ;
    Vector3 leftDirection ;
    Vector3 rightDirection;
    touchingSide result;

    enum touchingSide
    {
        front,
        back,
        left,
        right
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enermyPos = transform.position;

        frontDirection = transform.up;
        backDirection = -transform.up;
        leftDirection = -transform.right;
        rightDirection = transform.right;

        Vector3 m_frontlLine_end = enermyPos + frontDirection * 2.0f;
        Vector3 m_backLine_end = enermyPos + backDirection * 2.0f;
        Vector3 m_leftLine_end = enermyPos + leftDirection * 2.0f;
        Vector3 m_rightLine_end = enermyPos + rightDirection * 2.0f;

        Debug.DrawLine(enermyPos, m_frontlLine_end, Color.green);
        Debug.DrawLine(enermyPos, m_backLine_end, Color.yellow);
        Debug.DrawLine(enermyPos, m_leftLine_end, Color.red);
        Debug.DrawLine(enermyPos, m_rightLine_end, Color.black);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 shootingAt = collision.transform.position;
        Vector3 shootingDir = shootingAt - enermyPos;

        float max = -1f;
        

        if(Vector3.Dot(frontDirection, shootingDir) > max)
        {
            max = Vector3.Dot(frontDirection, shootingDir);
            result = touchingSide.front;
        }
        if (Vector3.Dot(leftDirection, shootingDir) > max)
        {
            max = Vector3.Dot(leftDirection, shootingDir);
            result = touchingSide.left;
        }
        if (Vector3.Dot(rightDirection, shootingDir) > max)
        {
            max = Vector3.Dot(rightDirection, shootingDir);
            result = touchingSide.right;
        }
        if (Vector3.Dot(backDirection, shootingDir) > max)
        {
            max = Vector3.Dot(backDirection, shootingDir);
            result = touchingSide.back;
        }

        if(result == touchingSide.front)
        {
            if(frontShield)
            {
                gameObject.GetComponent<guardEnermy>().isTouchSheild = true;
            }
            Debug.Log("hit front");
        }
        if (result == touchingSide.back)
        {
            if (backShield)
            {
                gameObject.GetComponent<guardEnermy>().isTouchSheild = true;
            }
            Debug.Log("hit back");
        }
        if (result == touchingSide.left)
        {
            if (leftShield)
            {
                gameObject.GetComponent<guardEnermy>().isTouchSheild = true;
            }
            Debug.Log("hit left");
        }
        if (result == touchingSide.right)
        {

            if (rightShield)
            {
                gameObject.GetComponent<guardEnermy>().isTouchSheild = true;
            }
            Debug.Log("hit right");
        }
    }

    
}
