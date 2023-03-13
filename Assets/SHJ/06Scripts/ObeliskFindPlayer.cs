using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskFindPlayer : MonoBehaviour
{
    //public OVRCameraRig cameraRig;
    public new GameObject camera;
    public GameObject cage;
    public GameObject enemy;

    bool isTouch = false;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouch)
        {
            // ī�޶� ��ȯ: camera
            camera.SetActive(true);
            // �÷��̾� ������ ���� : OVRCameraRig, PlayerMove ��ũ��Ʈ
            //cameraRig.enabled = false;
            // ī�޶� ����
            currentTime += Time.deltaTime;
            if (currentTime < 4)
            {
                camera.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            }
            // ö�� �ö�
            if (cage.transform.position.y < 15)
            {
                cage.transform.position += Vector3.up * 2 * Time.deltaTime;
            }
            // ���� ���� ��� �ѱ�: Appear()
            Enemy_Boss.instance.Appear();
        }
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
            isTouch = true;
        }
    }

}
