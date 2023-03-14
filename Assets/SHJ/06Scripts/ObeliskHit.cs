using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� -> ���� ��Ż ��Ʈ��Ʈ
public class ObeliskHit : MonoBehaviour
{

    // ���� �� ��������
    public Transform DungeonPos;
    public Transform villagePos;

    // ����Ʈ
    public GameObject quest00; // �ڷ���Ʈ �õ��� quest0 ����
    public GameObject quest07; // �ڷ���Ʈ �õ��� quest7 ����

    // ����Ʈ �ο� �� ������ ���� -> ���� �� ��Ż �̵� ����
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
            // �ٽ� ���� ������ �̵��ϴ� ���
            inDungeon = true;
            quest07.SetActive(false);
            StartCoroutine(Teleport(other));
        }

        //ī�޶� ����
        //���� ����

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
