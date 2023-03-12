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
            other.GetComponent<PlayerMove>().enabled = false;
            OnAction();
        }
    }

    void OnAction()
    {
        // 카메라 전환: camera
        camera.enabled = true;
        // 플레이어 움직임 제한 : OVRCameraRig, PlayerMove 스크립트
        cameraRig.enabled = false;
        // 철장 올라감
        if (cage.transform.position.y < 30)
        {
            cage.transform.position += Vector3.up * 2 * Time.deltaTime;
        }
        // 적의 등장 기능 켜기: Appear()

    }
}
