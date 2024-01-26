using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier_c : MonoBehaviour
{
    Vector3 m_normal_end;
    Vector3 m_pos;
    Vector3 m_shootLeftDir;
    Vector3 m_shootRightDir;
    public float m_rotateAngle = 10.0f;

    public GameObject center;
    public GameObject outputLeft;
    public GameObject outputRight;
    public GameObject leftPathPoint;
    public GameObject midPathPoint;
    public GameObject rightPathPoint;

    Vector3 frontDirection;
    Vector3 backDirection;

    Vector2 outputLeftPos;
    Vector2 outputRightPos;

    Vector2 leftPathPointPos;
    Vector2 midPathPointPos;
    Vector2 rightPathPointPos;

    GameObject theApp;

    enum touchingSide
    {
        front,
        back,
        left,
        right
    }

    touchingSide result;
    // Start is called before the first frame update
    void Start()
    {
        outputLeftPos = outputLeft.transform.position;
        outputRightPos = outputRight.transform.position;

        leftPathPointPos = leftPathPoint.transform.position;
        rightPathPointPos = rightPathPoint.transform.position;
        midPathPointPos = midPathPoint.transform.position;

        frontDirection = transform.up;
        backDirection = -transform.up;

        theApp = GameObject.Find("App");

    }

    // Update is called once per frame
    void Update()
    {
        m_shootLeftDir = GetRotatedDegrees(m_rotateAngle, -transform.right);
        m_shootRightDir = GetRotatedDegrees(-m_rotateAngle, transform.right);
        //--------------------------------------------------------------------------------------------- DEBUG LINE
        m_pos = transform.position;
        Vector3 direction = -transform.up;
        m_normal_end = m_pos + direction * 2.0f;
        Vector3 tempEndPointL = m_pos + m_shootLeftDir * 10.0f;
        Vector3 tempEndPointR = m_pos + m_shootRightDir * 10.0f;


        Debug.DrawLine(m_pos, m_normal_end, Color.green);
        Debug.DrawLine(m_pos, tempEndPointL, Color.blue);
        Debug.DrawLine(m_pos, tempEndPointR, Color.red);
        //---------------------------------------------------------------------------------------------


    }
    IEnumerator doOutput(Collider2D collision, int satuationIndex)
    {
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Rigidbody2D projectileRigbody = collision.gameObject.GetComponent<Rigidbody2D>();
        float projectileSpeed = collision.gameObject.GetComponent<projectile>().projectileSpeed;
        float delaySec = 0.02f;
        yield return new WaitForSeconds(delaySec);
        switch(satuationIndex)
        {
            case 1:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = leftPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = midPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = rightPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputRightPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootRightDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 2:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = midPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = rightPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputRightPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootRightDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 3:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = rightPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputRightPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootRightDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 4:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = outputRightPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootRightDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 5:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = rightPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = midPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = leftPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputLeftPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootLeftDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 6:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = midPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = leftPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputLeftPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootLeftDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 7:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = leftPathPointPos;
                yield return new WaitForSeconds(delaySec);
                collision.transform.position = outputLeftPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootLeftDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;

            case 8:
                collision.gameObject.GetComponent<projectile>().isDoingCurve = true;
                collision.transform.position = outputLeftPos;
                projectileRigbody.velocity = projectileSpeed * (Vector2)m_shootLeftDir;
                collision.gameObject.GetComponent<projectile>().isDoingCurve = false;
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 shootingAt = collision.transform.position;
        Vector3 shootingDir = shootingAt - center.transform.position;

        float max = -1f;


        if (Vector3.Dot(frontDirection, shootingDir) > max)
        {
            max = Vector3.Dot(frontDirection, shootingDir);
            result = touchingSide.front;
        }
        
        if (Vector3.Dot(backDirection, shootingDir) > max)
        {
            max = Vector3.Dot(backDirection, shootingDir);
            result = touchingSide.back;
        }

        if (result == touchingSide.front)
        {
            if(collision.gameObject.CompareTag("projectile"))
            {
                if(collision.gameObject.GetComponent<projectile>().isDoingCurve == false)
                {
                    Destroy(collision.gameObject);
                }
            }
            return;
        }

            if (collision.gameObject.CompareTag("projectile"))
        {
            if (theApp)
            {
                if (!theApp.GetComponent<appSystem>().isMutedSoundEffect)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
        else
        {
            return;
        }
        Vector3 lineYAxis = GetObjXAxis();
        Vector3 projectileMovingLine = GetColliderObjEnterLine(collision);
        float angle = Vector3.Angle(transform.right, projectileMovingLine);

        //float dir = (Vector3.Dot(Vector3.up, Vector3.Cross(lineYAxis, projectileMovingLine)) < 0 ? -1 : 1);
        //angle *= dir;
        Debug.Log("Angle is "+ angle);

        Rigidbody2D projectileRigbody = collision.gameObject.GetComponent<Rigidbody2D>();
        float projectileSpeed = collision.gameObject.GetComponent<projectile>().projectileSpeed;
        if (angle < 90f)
        {
            if (Vector2.Distance(outputLeftPos, collision.transform.position)  < Vector2.Distance(outputRightPos, collision.transform.position))
            {
                //left
                if (Vector2.Distance(outputLeftPos, collision.transform.position) < Vector2.Distance(midPathPointPos, collision.transform.position))
                {
                    //left
                    // go to path point 1 2 3
                    StartCoroutine(doOutput(collision, 1));
                    
                }
                else
                {
                    //midpoint;
                    // go to path point mid 3
                    StartCoroutine(doOutput(collision, 2));
                }
            }
            else
            {
                //right
                if (Vector2.Distance(outputRightPos, collision.transform.position) < Vector2.Distance(midPathPointPos, collision.transform.position))
                {
                    //right
                    // go to path point 
                    StartCoroutine(doOutput(collision, 4));
                }
                else
                {
                    //midpoint;
                    // go to path point 3
                    StartCoroutine(doOutput(collision, 3));
                }
            }
            
            
        }
        else if(angle > 90f)
        {
            if (Vector2.Distance(outputLeftPos, collision.transform.position) > Vector2.Distance(outputRightPos, collision.transform.position))
            {
                //right
                if (Vector2.Distance(outputRightPos, collision.transform.position) < Vector2.Distance(midPathPointPos, collision.transform.position))
                {
                    //right
                    // go to path point   
                    StartCoroutine(doOutput(collision, 5));

                }
                else
                {
                    //midpoint;
                    // go to path point 1
                    StartCoroutine(doOutput(collision, 6));
                }
            }
            else
            {
                //left
                if (Vector2.Distance(outputLeftPos, collision.transform.position) < Vector2.Distance(midPathPointPos, collision.transform.position))
                {
                    //right
                    // go to path point 321
                    StartCoroutine(doOutput(collision, 8));
                }
                else
                {
                    //midpoint;
                    // go to path point 21
                    StartCoroutine(doOutput(collision, 7));
                }
            }
            //Debug.Log("left");
            //projectileRigbody.velocity = projectileSpeed  * (Vector2)m_shootLeftDir;
        }
    }

    private Vector3 GetObjXAxis()
    {
        m_pos = transform.position;
        Vector3 direction = transform.right;
        m_normal_end = m_pos + direction * 2.0f;
        return m_normal_end - m_pos;
    }

    private Vector3 GetColliderObjEnterLine(Collider2D collision)
    {
        Vector3 ColPos = collision.transform.position;
        Vector3 direction = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        m_normal_end = ColPos + direction * 2.0f;
        return m_normal_end - ColPos;
    }   

    private Vector2 GetRotatedDegrees(float degrees, Vector2 obj)
    {
        float R = obj.magnitude;
        float theta = Mathf.Atan2(obj.y, obj.x) + degrees* Mathf.Deg2Rad;
        return new Vector2(R * Mathf.Cos(theta), R * Mathf.Sin(theta));
    }
}
