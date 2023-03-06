using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskHit : MonoBehaviour
{
    // ���� ��������
    public Transform startPos;
    public GameObject quest;

    // ����Ʈ �ο� �� ������ ���� -> ���� �� ��Ż �̵� ����
    public bool isQuest = false;
    // ������ �׾��� ��, �����ʿ����� ��Ż ����, ���϶� �̵� ����
    public bool bossDead = false;


    private void OnTriggerEnter(Collider other)
    {
        if (isQuest && other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
            quest.SetActive(false);

        }
        else if (bossDead)
        {
            // ������ ������ ũ����Ż ����
            // �ٽ� ���� ������ �̵��ϴ� ���
        }

        //ī�޶� ����
        //���� ����

    }


    IEnumerator Teleport(Collider other)
    {
        yield return new WaitForSeconds(2f);
        CharacterController cc = other.GetComponent<CharacterController>();
        cc.enabled = false;
        other.gameObject.transform.position = startPos.position;
        other.gameObject.transform.rotation = startPos.rotation;
        cc.enabled = true;
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
    //            // ������ ������ ũ����Ż ����
    //            // �ٽ� ���� ������ �̵��ϴ� ���
    //        }

    //        // ī�޶� ����
    //        // ���� ����
    //    }
    //}
}
