using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// ���� �ڵ�Ʈ���Ÿ� ������ �� �ݴ��� ��Ʈ�ѷ����� �Ÿ��� 2M ���϶��
// �տ� ��ź(GripObject)�� ���ʹ�.
// ���� �ڵ�Ʈ���Ÿ� ���� �� �տ� ��ź�� �ִٸ� ����ʹ�.
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
        // ���� �ڵ�Ʈ���Ÿ� ���� �� 
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controlller))
        {
            DoThrow();
        }

        // ���� �ڵ�Ʈ���Ÿ� ������ ��
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
        // �տ� ��ź�� �ִٸ�
        if (null != gripObj)
        {
            grab.GetComponent<BOw>().isGrap = false;
            // ����ʹ�.
            // �θ��ڽ� ���踦 ����
            gripObj.transform.parent = null;
            // ��ź�� ������ �Ѱ�
            Rigidbody arrowRB = gripObj.GetComponent<Rigidbody>();
            arrowRB.isKinematic = false;
            Vector3 handVector = transform.position - grabOrigin.transform.position;
            float force = Mathf.Min(1, handVector.magnitude) * 100;
            arrowRB.AddForce(-dir * force, ForceMode.Impulse);
            // �������� "��ü�� ���Ҵ�" ��� �ϰ�ʹ�.
            gripObj = null;
        }
    }
    private void DoGrab()
    {
        // �ݴ��� ��Ʈ�ѷ����� �Ÿ��� 2M ���϶��
        float dist = Vector3.Distance(transform.position, otherHand.position);
        if (dist < 0.2f)
        {
            grab.GetComponent<BOw>().isGrap = true;
            // �տ� ��ź(GripObject)�� ���ʹ�.
            GameObject arrow = Instantiate(arrowFactory);
            // ���� �θ� = ��
            arrow.transform.parent = grab.transform;
            arrow.transform.localPosition = Vector3.zero;
            Vector3 forwardVector = arrowPos.transform.position - grabOrigin.transform.position;
            forwardVector.Normalize(); 
            arrow.transform.forward = forwardVector;
            // bomb�� ������ ����ʹ�.
            Rigidbody bombRB = arrow.GetComponent<Rigidbody>();
            bombRB.isKinematic = true;
            // �տ� �� ��ź�� ����ϰ�ʹ�.
            gripObj = arrow;
        }
    }
}
