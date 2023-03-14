using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// 만약 핸드트리거를 눌렀을 때 반대쪽 컨트롤러와의 거리가 2M 이하라면
// 손에 폭탄(GripObject)을 쥐고싶다.
// 만약 핸드트리거를 뗏을 때 손에 폭탄이 있다면 놓고싶다.
public class HandGrip : MonoBehaviour
{
    public OVRInput.Controller controlller;
    public Transform otherHand;
    public GameObject arrowFactory;
    public GameObject grab; // == bow
    public GameObject grabOrigin;
    public Transform arrowPos;
    public float speed = 10f;
    Vector3 dir;
    //bool canGrip = false;
    //GameObject bow;
    GameObject gripObj;
    // Start is called before the first frame update
    void Start()
    {
        //bow = GameObject.Find("BowLineCenter");
    }

    float forceRate = 0;
    // Update is called once per frame
    void Update()
    {
        // 만약 핸드트리거를 뗏을 때 
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoThrow();
        }

        // 만약 핸드트리거를 눌렀을 때
        if (false == gripObj && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            Vector3 dis = grab.transform.position - transform.position;
            if (dis.magnitude < 0.3f)
            {
                DoGrab();
            }
        }

        if (null != gripObj)
        {
            Vector3 handVector = transform.position - grabOrigin.transform.position;
            float dot = Vector3.Dot(grabOrigin.transform.forward, handVector);
            if (dot < 0)
            {
                forceRate = Mathf.Min(1, handVector.magnitude * 2);
            }
            else
            {
                forceRate = 0;
            }
            dir = grabOrigin.transform.position - arrowPos.transform.position;
            dir.Normalize();
            grab.transform.localPosition = Vector3.zero + dir * forceRate * 2;
            gripObj.transform.forward = -dir;
        }

    }

    private void DoThrow()
    {
        // 손에 폭탄이 있다면
        if (null != gripObj)
        {
            grab.GetComponent<BOw>().isGrap = false;
            // 놓고싶다.
            // 부모자식 관계를 끊고
            gripObj.transform.parent = null;
            // 폭탄의 물리를 켜고
            Rigidbody arrowRB = gripObj.GetComponent<Rigidbody>();
            arrowRB.isKinematic = false;
            Vector3 handVector = transform.position - grabOrigin.transform.position;
            float force = Mathf.Min(1, handVector.magnitude) * 100;
            arrowRB.AddForce(-dir * force, ForceMode.Impulse);
            // 던졌으면 "물체를 놓았다" 라고 하고싶다.
            gripObj = null;
        }
    }
    private void DoGrab()
    {
        // 반대쪽 컨트롤러와의 거리가 2M 이하라면
        float dist = Vector3.Distance(transform.position, otherHand.position);
        if (dist < 0.2f)
        {
            grab.GetComponent<BOw>().isGrap = true;
            // 손에 폭탄(GripObject)을 쥐고싶다.
            GameObject arrow = Instantiate(arrowFactory);
            // 나의 부모 = 너
            arrow.transform.parent = grab.transform;
            arrow.transform.localPosition = Vector3.zero;
            Vector3 forwardVector = arrowPos.transform.position - grabOrigin.transform.position;
            forwardVector.Normalize(); 
            arrow.transform.forward = forwardVector;
            // bomb의 물리를 끄고싶다.
            Rigidbody bombRB = arrow.GetComponent<Rigidbody>();
            bombRB.isKinematic = true;
            // 손에 쥔 폭탄을 기억하고싶다.
            gripObj = arrow;
        }
    }
}
