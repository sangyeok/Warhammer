using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskExplore : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public Camera camera;
    public GameObject cage;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    // <���� ���� ���>
    // ���ڸ� ���Ÿ���
    // �÷��̾� �����ϸ�(������ �� �ݰ�ȿ� ������)
    // 0. �÷��̾� ī�޶� �� ������ ����
    // 1. ī�޶� ��ȯ
    // 2. ö�� �ö�


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().enabled = false;
            OnAction();
        }
    }

    void OnAction()
    {
        // ī�޶� ��ȯ: camera
        camera.enabled = true;
        // �÷��̾� ������ ���� : OVRCameraRig, PlayerMove ��ũ��Ʈ
        cameraRig.enabled = false;
        // ö�� �ö�
        if (cage.transform.position.y < 30)
        {
            cage.transform.position += Vector3.up * 2 * Time.deltaTime;
        }
        // ���� ���� ��� �ѱ�: Appear()

    }
}
