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

    public GameObject quest05; // 트리거 닿는 순간 quest5 끄기

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            // 카메라 전환: camera
            camera.SetActive(true);

            // 철장 올라감
            if (cage.transform.position.y < 15)
            {
                cage.transform.position += Vector3.up * 2 * Time.deltaTime;
            }
            // 카메라 줌인
            currentTime += Time.deltaTime;
            if (currentTime < 4)
            {
                camera.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            }
            if(currentTime > 5)
            {
                // 적의 등장 기능 켜기: Appear
                Enemy_Boss.instance.m_State = Enemy_Boss.EnemyState.Appear;
                BoxCollider box = GetComponent<BoxCollider>();
                box.enabled = false;
                particle.SetActive(false);
            }
        }

        // 보스가 사망하면 파티클 키기
        if(Enemy_Boss.instance.isDie)
        {
            particle.SetActive(true);
        }
    }



    // <보스 등장 방식>
    // 제자리 흔들거리기
    // 플레이어 감지하면(빛나는 돌 반경안에 들어오면)
    // 0. 플레이어 카메라 및 움직임 정지
    // 1. 카메라 전환
    // 2. 철장 올라감

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 움직임 제한 : PlayerMove 스크립트
            other.GetComponent<PlayerMove>().enabled = false;
            quest05.SetActive(false);
            isTouch = true;
        }

    }

}
