using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

// 마을 -> 던전 포탈 스트립트
public class ObeliskHit : MonoBehaviour
{

    // 던전 및 마을지점
    public Transform DungeonPos;
    public Transform villagePos;

    public GameObject particle;

    // 퀘스트
    public GameObject quest00; // 텔레포트 시도시 quest0 끄기
    public GameObject quest07; // 텔레포트 시도시 quest7 끄기

    // 퀘스트 부여 후 참으로 변경 -> 참일 떄 포탈 이동 가능
    public bool isQuest = false;
    bool inVillage = false;
    bool inDungeon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isQuest && other.CompareTag("Player"))
        {
            inVillage = true;
            quest00.SetActive(false);
            StartCoroutine(Teleport(other));

        }
        else if (Enemy_Boss.instance.isDie)
        {
            // 다시 마을 맵으로 이동하는 기능
            inDungeon = true;
            quest07.SetActive(false);
            BoxCollider box = GetComponent<BoxCollider>();
            box.enabled = false;
            particle.SetActive(false);
            StartCoroutine(Teleport(other));
        }


    }


    IEnumerator Teleport(Collider other)
    {
        yield return new WaitForSeconds(2f);
        CharacterController cc = other.GetComponent<CharacterController>();
        cc.enabled = false;
        if (inVillage)
        {
            other.gameObject.transform.position = DungeonPos.position;
            other.gameObject.transform.rotation = DungeonPos.rotation; 

        }
        else if (inDungeon)
        {
            other.gameObject.transform.position = villagePos.position;
            other.gameObject.transform.rotation = villagePos.rotation;

        }
        cc.enabled = true;
    }
}
