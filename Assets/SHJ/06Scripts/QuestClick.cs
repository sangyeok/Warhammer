using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class QuestClick : MonoBehaviour
{
    public GameObject potal;

    // ����Ʈ

    public GameObject quest00; // ����Ʈ ���� ui ������ quest0 �ѱ�
    public GameObject quest01; // ����Ʈ ���� ui ������ quest1 ����

    private void OnTriggerEnter(Collider other)
    {
        potal.GetComponent<ObeliskHit>().isQuest = true;
        quest01.SetActive(true);
        gameObject.SetActive(false);

    }
}
