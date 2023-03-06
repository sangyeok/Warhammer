using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskHit : MonoBehaviour
{
    // 던전 시작지점
    public GameObject startPos;

    // 퀘스트 부여 후 참으로 변경 -> 참일 떄 포탈 이동 가능
    public bool isQuest = false;
    // 보스가 죽었을 때, 보스맵에서의 포탈 생성, 참일때 이동 가능
    public bool bossDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (isQuest)
            {
                other.gameObject.transform.position = startPos.transform.position;
                other.gameObject.transform.rotation = startPos.transform.rotation;
            }
            else if (bossDead)
            {
                // 보스가 죽으면 크리스탈 등장
                // 다시 마을 맵으로 이동하는 기능
            }

            // 카메라 무빙
            // 보스 등장
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
        
    //    if (cols[0].gameObject.CompareTag("Player"))
    //    {
    //        print(1111111111111111);
    //        print(cols[0].name);
    //        if (isQuest)
    //        {
    //            print(22222222222221);
    //            StartCoroutine(Teleport());
    //        }
    //        else if(bossDead)
    //        {
    //            // 보스가 죽으면 크리스탈 등장
    //            // 다시 마을 맵으로 이동하는 기능
    //        }

    //        // 카메라 무빙
    //        // 보스 등장
    //    }
    //}
}
