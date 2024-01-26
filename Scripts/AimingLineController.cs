using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AimingLineController : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public int reflections;
    public float maxLength;
    public GameObject left;
    public GameObject right;

    LineRenderer _lineRenderer;
    Ray2D ray;
    RaycastHit2D hit;
    Ray2D rayLeft;
    RaycastHit2D hitLeft;
    Ray2D rayRight;
    RaycastHit2D hitRight;

    //private Scene predictionScene;
    //private PhysicsScene predicitonPhysicsScene;

    //private static Scene currentScene = SceneManager.GetActiveScene();
    //private PhysicsScene currentPhysicsScene = currentScene.GetPhysicsScene();
    [HideInInspector]
    public Vector2 justTouchPos;
    [HideInInspector]
    public Vector2 bounceBackPos;
    public float factor = 0.6f;
    Vector2 dir;
    Vector2 initialPos;
    public GameObject TrackBall;
    private Vector2 recordDir;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        //CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        //predictionScene = SceneManager.CreateScene("Prediction", parameters);
        //predicitonPhysicsScene = predictionScene.GetPhysicsScene();
    }

    
    void Update()
    {
        Vector2 leftPos = left.transform.position;
        Vector2 rightPos = right.transform.position;
        Vector2 leftDisplacement = (Vector2)transform.position - leftPos;
        Vector2 rightDisplacement = (Vector2)transform.position - rightPos;

        ray = new Ray2D(transform.position, transform.up);
        rayLeft = new Ray2D(leftPos, left.transform.up);
        rayRight = new Ray2D(rightPos, right.transform.up);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainLength = maxLength;

        hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f, ignoreLayer);
        hitLeft = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, 1000f, ignoreLayer);
        hitRight = Physics2D.Raycast(rayRight.origin, rayRight.direction, 1000f, ignoreLayer);

        Debug.DrawRay(ray.origin, ray.direction, Color.red,0.1f);
        Debug.DrawRay(rayLeft.origin, rayLeft.direction, Color.green, 0.1f);
        Debug.DrawRay(rayRight.origin, rayRight.direction, Color.blue, 0.1f);
        Debug.DrawLine(rayLeft.origin, hitLeft.point);
        Debug.DrawLine(rayRight.origin, hitRight.point);

        if (hit.collider || hitLeft.collider || hitRight.collider)
            {
                
           
            Vector2 leftHitPos = hitLeft.collider.transform.position;
            Vector2 rightHitPos = hitRight.collider.transform.position;
            Vector2 hitPos = hit.collider.transform.position;

            dir = (hit.point - (Vector2)transform.position).normalized;

                if(hit.collider.gameObject == hitLeft.collider.gameObject == hitRight.collider.gameObject)
            {
                initialPos = hit.point - dir * factor;

            }
            if (Vector2.Distance(leftHitPos, rayLeft.origin) < Vector2.Distance(rightHitPos, rayLeft.origin))
            {
                //left hit
                initialPos = hitLeft.point - dir * factor;
                initialPos += leftDisplacement;
            }
            if (Vector2.Distance(leftHitPos, rayRight.origin) > Vector2.Distance(rightHitPos, rayRight.origin))
            {
                //right hit
                initialPos = hitRight.point - dir * factor;
                initialPos += rightDisplacement;
            }






            if (_lineRenderer.enabled && Time.timeScale == 1)
                {
                    GameObject projectileTrackBall = Instantiate(TrackBall, initialPos, transform.rotation);
                    var script = projectileTrackBall.GetComponent<projectile>();
                    script.m_direction = dir;
                }

            _lineRenderer.positionCount += 1;

            if (hit.collider == hitLeft.collider == hitRight.collider)
            {
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

            }
            if (Vector2.Distance(leftHitPos, rayLeft.origin) < Vector2.Distance(rightHitPos, rayLeft.origin))
            {
                //left hit
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hitLeft.point);
            }
            if (Vector2.Distance(leftHitPos, rayRight.origin) > Vector2.Distance(rightHitPos, rayRight.origin))
            {
                //right hit
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hitRight.point);
            }

                   // ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, justTouchPos));

                if (hit.collider.tag != "wall" && hitLeft.collider.tag != "wall" && hitRight.collider.tag != "wall")
                {
                  
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                }
            else if (hit.collider.tag == "projectile" || hitLeft.collider.tag == "projectile" || hitRight.collider.tag == "projectile")
            {

                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
            }
            else
                {
                Vector2 bouceBackDir = (bounceBackPos - justTouchPos).normalized;  
                /*Vector2 endPos = justTouchPos*/
                _lineRenderer.positionCount += 1;
                if (hit.collider == hitLeft.collider == hitRight.collider)
                {
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point + bouceBackDir * remainLength/*ray.origin + ray.direction * remainLength*/);

                }
                if (Vector2.Distance(leftHitPos, rayLeft.origin) < Vector2.Distance(rightHitPos, rayLeft.origin))
                {
                    //left hit
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hitLeft.point + bouceBackDir * remainLength/*ray.origin + ray.direction * remainLength*/);
                }
                if (Vector2.Distance(leftHitPos, rayRight.origin) > Vector2.Distance(rightHitPos, rayRight.origin))
                {
                    //right hit
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hitRight.point + bouceBackDir * remainLength/*ray.origin + ray.direction * remainLength*/);
                }
                
                }       
            }
           
        
    }

    //private void FixedUpdate()
    //{
    //    if(currentPhysicsScene.IsValid())
    //    {
    //        currentPhysicsScene.Simulate(Time.fixedDeltaTime);
    //    }
    //}
}
