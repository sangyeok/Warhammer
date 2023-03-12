using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bow : MonoBehaviour
{
    // �� ��� ��ġ
    public Transform pullTransform;
    Vector3 GrabPos;

    public Transform leftHand;
    public Transform rightHand;
    Vector3 leftPos;

    // �Է� ���� Ʈ���� ��ư
    float indexTriggerL;
    float indexTriggerR;

    // �Է� Evnet
    //public UnityEvent startedGrabEvent;

    // ����ִ��� Ȯ��
    bool canGrab = false;
    bool isGrab = false;

    public Transform indicator;

    // Ʈ���� �ڽ��� �޼��� ������ ��� ����
    // ��� ���ɻ��¿��� Ʈ���� ��ư�� ������ ������ ����
    // Ȱ�� �߾��� �޼� ����� ����

    // GrabPoint�� ���� ������ pull ����
    // pull ���ɻ��¿��� Ʈ���� ��ư�� ������ ȭ�� ����(ȭ�� ���� GrabPoint��)

    // ���� �������,?




    // Start is called before the first frame update
    void Start()
    {
        GrabPos = pullTransform.position;
        leftPos = leftHand.position;
        indexTriggerL = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        indexTriggerR = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Ʈ���� �ڽ��� �޼��� ������ ��� ���� + �ε������� ���̱�
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �޼�, �������� Player
        {
            canGrab= true;
            indicator.GetComponent<MeshRenderer>().enabled= true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canGrab = false;
            indicator.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void StartGrab()
    {
        // ��� ���� ��Ȳ���� Ʈ���� ��ư Ŭ�� ��, Ȱ�� �տ� ����
        if (indexTriggerL > 0.5 && canGrab)
        {
            transform.position = leftPos;
        }
        isGrab= true;
    }

    void EndGrab()
    {
        if(indexTriggerL < 0.3 && !isGrab)
        {
            // Ȱ �ٴڿ� ������
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity= true;
        }
    }


}
