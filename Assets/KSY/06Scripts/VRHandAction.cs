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
        // ������ ��Ʈ�ѷ��� �ڵ�Ʈ���Ÿ� ���� ���, ���� �����ȿ� �ִ� ��ü�� ��� �ʹ�.
        // Grab Ʈ���Ÿ��� �����ٸ�
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            // Grab Ʈ���Ÿ� ������ �� ��Ʈ�ѷ��� ��ġ���� ���� �� �ִ� �Ÿ���ŭ �ȿ� �ִ� ��� ������Ʈ�� �浹ü�� ���´�.
            Collider[] hittedObjects = Physics.OverlapSphere(this.transform.position, grabRange);

            // ���� ����� ������Ʈ�� �ε���
            int closest = 0;

            // ������ ��Ʈ�ѷ��� ���� ����� ������Ʈ�� �����Ѵ�
            for (int i = 1; i < hittedObjects.Length; i++)
            {
                // �հ� ���� ����� ��ü�� �Ÿ�
                Vector3 closestPos = hittedObjects[closest].transform.position;

                float closestDistance = Vector3.Distance(closestPos, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

                // ���� ��ü���� �Ÿ�
                Vector3 nextPos = hittedObjects[i].transform.position;

                float nextDistance = Vector3.Distance(nextPos, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

                // ���� ���� ��ü�� �Ÿ��� �� �����ٸ� ���� ����� �ε����� ����� ���� ����
                if (nextDistance < closestDistance)
                    closest = i;
            }

            if (hittedObjects.Length > 0)
            {
                // ���� ���·� ��ȯ
                isGrabbed = true;


                // ���� ��ü�� ���� ���
                grabbedObject = hittedObjects[closest].gameObject;

                grabbedObject.transform.position = transform.position;

                // ���� ��ü�� ���� �ڽ����� ���
                //grabbedObject.transform.parent = rController;

                //grabbedObject.transform.forward = rController.forward;

                // ���� ��ü�� ������� ����
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
        // Grab Ʈ���Ÿ� ���Ҵٸ�
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            // ��� ���� ���� ���·� ��ȯ
            isGrabbed = false;

            // ���� ��� Ȱ��ȭ
            //grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            // �տ��� ��Ҵ� ������Ʈ ����
            grabbedObject.transform.parent = null;

            // �� ������Ʈ�� �������� �ʱ�
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