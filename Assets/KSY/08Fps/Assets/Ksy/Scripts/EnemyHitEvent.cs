using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ �� �̺�Ʈ�� ȣ��Ǹ� ó���� Ŭ����

public class EnemyHitEvent : MonoBehaviour
{
    void OnHit()
    {
        playerHealth.Instance.HP--;
        //playerHealth.Instance.SetHp(playerHealth.Instance.GetHp() - 1);
    }
}
