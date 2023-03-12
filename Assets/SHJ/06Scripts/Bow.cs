using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bow : MonoBehaviour
{
    // 손 잡는 위치
    public Transform pullTransform;
    Vector3 GrabPos;

    public Transform leftHand;
    public Transform rightHand;
    Vector3 leftPos;

    // 입력 받을 트리거 버튼
    float indexTriggerL;
    float indexTriggerR;

    // 입력 Evnet
    //public UnityEvent startedGrabEvent;

    // 잡고있는지 확인
    bool canGrab = false;
    bool isGrab = false;

    public Transform indicator;

    // 트리거 박스에 왼손이 닿으면 잡기 가능
    // 잡기 가능상태에서 트리거 버튼을 누르면 누르는 동안
    // 활의 중앙이 왼손 가운데에 붙음

    // GrabPoint에 손이 닿으면 pull 가능
    // pull 가능상태에서 트리거 버튼을 누르면 화살 생성(화살 끝이 GrabPoint에)

    // 당기면 당겨지기,?




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

    // 트리거 박스에 왼손이 닿으면 잡기 가능 + 인디케이터 보이기
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 왼손, 오른손이 Player
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
        // 잡기 가능 상황에서 트리거 버튼 클릭 시, 활이 손에 붙음
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
            // 활 바닥에 버리기
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity= true;
        }
    }


}
