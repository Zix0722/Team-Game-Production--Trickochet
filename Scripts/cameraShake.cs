using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public float lightShakeParameter = 0.05f;
    public float midShakeParameter = 0.08f;
    public float strongShakeParameter = 0.12f;

    public GameObject mainCam;
    Vector3 originCamPos;

    float randCurrentOffsetLight;
    float randCurrentOffsetMid;
    float randCurrentOffsetStrong;
    //[HideInInspector]
    public bool isDoLight = false;
    //[HideInInspector]
    public bool isDoMid = false;
    //[HideInInspector]
    public bool isDoStrong = false;

    public float lightShakeSeconds = 0.1f;
    public float midShakeSeconds = 0.3f;
    public float strongShakeSeconds = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
        originCamPos = mainCam.transform.position;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(isDoLight)
        {
            randCurrentOffsetLight = Random.Range(-lightShakeParameter, lightShakeParameter);
            mainCam.transform.position = originCamPos + new Vector3(randCurrentOffsetLight, randCurrentOffsetLight, 0f);
        }
        if (isDoMid)
        {
            randCurrentOffsetMid = Random.Range(-midShakeParameter, midShakeParameter);

            mainCam.transform.position = originCamPos + new Vector3(randCurrentOffsetMid, randCurrentOffsetMid, 0f);
        }
        if (isDoStrong)
        {
            randCurrentOffsetStrong = Random.Range( -strongShakeParameter,  strongShakeParameter);
            mainCam.transform.position = originCamPos + new Vector3(randCurrentOffsetStrong, randCurrentOffsetStrong, 0f);
        }
    }

    public IEnumerator doLight()
    {
        isDoLight = true;
        yield return new WaitForSeconds(lightShakeSeconds);
        isDoLight = false;
    }

    public IEnumerator doMid()
    {
        isDoMid = true;
        yield return new WaitForSeconds(midShakeSeconds);
        isDoMid = false;
    }

    public IEnumerator doStrong()
    {
        isDoStrong = true;
        yield return new WaitForSeconds(strongShakeSeconds);
        isDoStrong = false;
    }

    public void doLightShake()
    {
        StartCoroutine(doLight());
    }
    public void doLMidShake()
    {
        StartCoroutine(doMid());
    }
    public void doStrongShake()
    {
        StartCoroutine(doStrong());
    }
}
