using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeRandom : MonoBehaviour
{
    public float camShakeTime = 0.5f;
    public float camShakeDelayTime = 0.02f;
    public float camShakeDistance = 0.205f;
    Vector3 startPos;
    float curCamShakeTime;
    float curCamShakeDelayTime;

    //
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RunCamShake();
    }

    public void OnStartShake()
    {
        isStart = true;
        curCamShakeTime = 0;
        curCamShakeDelayTime = 0;
        startPos = Camera.main.transform.localPosition;
    }

    private void RunCamShake()
    {
        if (isStart == false)
        {
            return;
        }
        curCamShakeTime += Time.deltaTime;
        if (curCamShakeTime < camShakeTime)
        {
            curCamShakeDelayTime += Time.deltaTime;
            if (curCamShakeDelayTime > camShakeDelayTime)
            {
                transform.localPosition = startPos + Random.insideUnitSphere
                * camShakeDistance * Time.deltaTime;
                curCamShakeDelayTime = 0;
            }
        }
        else
        {
            isStart = false;
        }
    }
}
