using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class QuestClick : MonoBehaviour
{
    public GameObject potal;

    // 퀘스트

    public GameObject quest00; // 퀘스트 선택 ui 끝나면 quest0 켜기
    public GameObject quest01; // 퀘스트 선택 ui 끝나면 quest1 끄기

    private void OnTriggerEnter(Collider other)
    {
        potal.GetComponent<ObeliskHit>().isQuest = true;
        quest01.SetActive(true);
        gameObject.SetActive(false);

    }
}
