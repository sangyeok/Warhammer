using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// 만약 핸드트리거를 눌렀을 때 반대쪽 컨트롤러와의 거리가 2M 이하라면
// 손에 폭탄(GripObject)을 쥐고싶다.
// 만약 핸드트리거를 뗏을 때 손에 폭탄이 있다면 놓고싶다.
public class HandGrip1 : MonoBehaviour
{
    public OVRInput.Controller controlller;
    public GameObject gameObject;
    //public Transform otherHand;
    //public GameObject readyFac;
    //public GameObject lazorFac;
    //public Transform lazorPos;
    //public GameObject sword;
    //bool isready = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //float curTime = 0;
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //Rigidbody rigidbody = sword.GetComponent<Rigidbody>();
        //rigidbody.velocity = OVRInput.GetLocalControllerVelocity(controlller);
        //print(rigidbody.velocity);
        //Vector3 dis = transform.position - otherHand.transform.position;
        // 만약 핸드트리거를 눌렀을 때
        //if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controlller))
        //{
        //    curTime += Time.deltaTime;
        //    if (dis.magnitude < 0.3f && curTime > 2)
        //    {
        //        curTime = 0;
        //        isready = true;
        //        GameObject ready = Instantiate(readyFac);
        //        ready.transform.parent = transform;
        //        ready.transform.localPosition = Vector3.zero;

        //        if(OVRInput.GetDown(OVRInput.Button.One, controlller) && isready)
        //        {
        //            GameObject lazor = Instantiate(lazorFac);
        //            lazor.transform.position = lazorPos.transform.position;
        //            lazor.transform.forward = lazorPos.transform.forward;
        //            isready = false;
        //        }
        //        if(OVRInput.GetDown(OVRInput.Button.Two, controlller))
        //        {

        //        }
        //    }
        //}
    }
}
