using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskFindPlayer : MonoBehaviour
{
    //public OVRCameraRig cameraRig;
    public new GameObject camera;
    public GameObject cage;
    public GameObject particle;

    bool isTouch = false;
    float currentTime;

    public GameObject quest05; // Ʈ���� ��� ���� quest5 ����

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            // ī�޶� ��ȯ: camera
            camera.SetActive(true);

            // ö�� �ö�
            if (cage.transform.position.y < 15)
            {
                cage.transform.position += Vector3.up * 2 * Time.deltaTime;
            }
            // ī�޶� ����
            currentTime += Time.deltaTime;
            if (currentTime < 4)
            {
                camera.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            }
            if(currentTime > 5)
            {
                // ���� ���� ��� �ѱ�: Appear
                Enemy_Boss.instance.m_State = Enemy_Boss.EnemyState.Appear;
                BoxCollider box = GetComponent<BoxCollider>();
                box.enabled = false;
                particle.SetActive(false);
            }
        }

        // ������ ����ϸ� ��ƼŬ Ű��
        if(Enemy_Boss.instance.isDie)
        {
            particle.SetActive(true);
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
            // �÷��̾� ������ ���� : PlayerMove ��ũ��Ʈ
            other.GetComponent<PlayerMove>().enabled = false;
            quest05.SetActive(false);
            isTouch = true;
        }

    }

}
