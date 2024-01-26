using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pierceing : MonoBehaviour
{
    public const int bounceTime = 3;
    bool m_canBounce = true;
    int m_haveBounced = 0;
    Rigidbody2D m_rb2D;
    public Vector3 m_direction;
    public float projectileSpeed = 2.0f;
    Vector3 currentPos;
    Vector3 lastFramePos;
    Vector3 currentDir;



    // Start is called before the first frame update
    void Start()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_rb2D.velocity = m_direction * projectileSpeed;
        StartCoroutine(getReady());
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //lastFramePos = currentPos;
        //currentPos = transform.position;

        //currentDir = (currentPos - lastFramePos).normalized;
        //Vector3 dir = transform.position + currentDir;

        //float angle = Mathf.Atan2(currentPos.y - lastFramePos.y, currentPos.x - lastFramePos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    private void FixedUpdate()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("softWall"))
        {
            stopMoving();
            return;   // to-do anim
        }
        else
        {

        }
        doBouncement();
    }

    private void doBouncement()
    {
        if (m_canBounce)
        {
            m_haveBounced++;
        }
        else
        {
            stopMoving();
        }
    }

    private void stopMoving()
    {
        m_rb2D.velocity = new Vector2(0.0f, 0.0f);
    }


    IEnumerator getReady()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 6;
    }
}
