using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskHit : MonoBehaviour
{
    // ���� ��������
    public GameObject startPos;

    // ����Ʈ �ο� �� ������ ���� -> ���� �� ��Ż �̵� ����
    public bool isQuest = false;
    // ������ �׾��� ��, �����ʿ����� ��Ż ����, ���϶� �̵� ����
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
                // ������ ������ ũ����Ż ����
                // �ٽ� ���� ������ �̵��ϴ� ���
            }

            // ī�޶� ����
            // ���� ����
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
    //            // ������ ������ ũ����Ż ����
    //            // �ٽ� ���� ������ �̵��ϴ� ���
    //        }

    //        // ī�޶� ����
    //        // ���� ����
    //    }
    //}
}
