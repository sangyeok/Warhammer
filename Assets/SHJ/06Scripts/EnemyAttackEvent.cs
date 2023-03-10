using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackEvent : MonoBehaviour
{
    // �÷��̾� HP ����
    // 1. Attack Animation Event
    // 2. Enemy Sprint ���¿��� �浹
    // 3. fire particle�� �浹
    Transform Target;

    private void Start()
    {
        //GameObject player = GameObject.Find("Player");
        //if (player != null)
        //{
        //    Target = player.transform;
        //}
    }

    public void OnHit()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        if(distance < 2f)
        {
            print("���� ������");
            playerHealth.Instance.HP--;
        }
    }
}
