using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// ���� �ڵ�Ʈ���Ÿ� ������ �� �ݴ��� ��Ʈ�ѷ����� �Ÿ��� 2M ���϶��
// �տ� ��ź(GripObject)�� ���ʹ�.
// ���� �ڵ�Ʈ���Ÿ� ���� �� �տ� ��ź�� �ִٸ� ����ʹ�.
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
        // ���� �ڵ�Ʈ���Ÿ� ������ ��
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
