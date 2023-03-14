using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandAction : MonoBehaviour
{
    bool isGrabbed = false;

    float grabRange = 0.2f;

    public Transform rController;
    public GameObject grabbedObject;

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed)
            TryGrab();
        else
            TryUngrab();

        // if (grabbedObject != null)
        // {
        //     if (grabbedObject.layer == 7 && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        //     {
        //        VRPistol.instance.OnShoot();
        //        PlayVibration(true, 50, 0.4f);
        //     }
        // }
        // else
        //     return;
    }

    private void TryGrab()
    {
        // 오른쪽 컨트롤러의 핸드트리거를 누를 경우, 일정 영역안에 있는 물체를 잡고 싶다.
        // Grab 트리거를을 눌렀다면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            // Grab 트리거를 눌렀을 때 컨트롤러의 위치에서 잡을 수 있는 거리만큼 안에 있는 모든 오브젝트의 충돌체를 얻어온다.
            Collider[] hittedObjects = Physics.OverlapSphere(this.transform.position, grabRange);

            // 가장 가까운 오브젝트의 인덱스
            int closest = 0;

            // 오른쪽 컨트롤러와 가장 가까운 오브젝트를 선택한다
            for (int i = 1; i < hittedObjects.Length; i++)
            {
                // 손과 가장 가까운 물체의 거리
                Vector3 closestPos = hittedObjects[closest].transform.position;

                float closestDistance = Vector3.Distance(closestPos, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

                // 다음 물체와의 거리
                Vector3 nextPos = hittedObjects[i].transform.position;

                float nextDistance = Vector3.Distance(nextPos, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

                // 만약 다음 물체의 거리가 더 가깝다면 가장 가까운 인덱스에 저장된 값을 변경
                if (nextDistance < closestDistance)
                    closest = i;
            }

            if (hittedObjects.Length > 0)
            {
                // 잡은 상태로 전환
                isGrabbed = true;


                // 잡은 물체에 대한 기억
                grabbedObject = hittedObjects[closest].gameObject;

                grabbedObject.transform.position = transform.position;

                // 잡은 물체를 손의 자식으로 기억
                //grabbedObject.transform.parent = rController;

                //grabbedObject.transform.forward = rController.forward;

                // 잡은 물체의 물리기능 정지
                //grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            else
            {
                return;
            }
        }
    }

    private void TryUngrab()
    {
        // Grab 트리거를 놓았다면
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            // 잡고 있지 않은 상태로 전환
            isGrabbed = false;

            // 물리 기능 활성화
            //grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            // 손에서 잡았던 오브젝트 놓기
            grabbedObject.transform.parent = null;

            // 그 오브젝트를 추적하지 않기
            grabbedObject = null;
        }
    }

    //public IEnumerator CreateVibrateTime(int iteration, int frequency, int strength, OVRInput.Controller controller, float time)
    //{
    //    var channel = controller == OVRInput.Controller.LTouch ? OVRHaptics.LeftChannel : OVRHaptics.RightChannel;

    //    byte[] sample = new byte[iteration];

    //    for (int i = 0; i < iteration; i++)
    //    {
    //        sample[i] = i % frequency == 0 ? (byte)0 : (byte)strength;
    //    }

    //    OVRHapticsClip createdClip = new OVRHapticsClip(sample, sample.Length);

    //    for (float t = 0; t <= time; t += Time.deltaTime)
    //    {
    //        Debug.Log("Play vib");
    //        channel.Queue(createdClip);
    //    }
    //    yield return new WaitForSeconds(time);
    //    channel.Clear();
    //    yield return null;
    //}

    //public void PlayVibration(bool rightHanded, int force, float time)
    //{
    //    if (rightHanded) StartCoroutine(CreateVibrateTime(64, 100, force, OVRInput.Controller.RTouch, time));
    //    else StartCoroutine(CreateVibrateTime(64, 100, force, OVRInput.Controller.LTouch, time));
    //}
}