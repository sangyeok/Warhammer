using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//최종
public class EnemyAttackEvent : MonoBehaviour
{
    // 플레이어 HP 감소
    // 1. Attack Animation Event
    // 2. Enemy Sprint 상태에서 충돌
    // 3. fire particle에 충돌

    Transform Target;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Target = player.transform;
        }
    }

    public float attackDis = 2f;
    public void OnMyHit()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        if(distance < attackDis)
        {
            playerHealth.Instance.HP--;
        }
    }
}
