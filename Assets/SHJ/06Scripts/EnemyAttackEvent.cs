using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    // �÷��̾� HP ����
    // 1. Attack Animation Event
    // 2. Enemy Sprint ���¿��� �浹
    // 3. fire particle�� �浹
    public void OnHit()
    {
        print("���� ������");
        playerHealth.Instance.HP--;
    }
}
