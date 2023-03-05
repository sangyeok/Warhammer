using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    // 플레이어 HP 감소
    // 1. Attack Animation Event
    // 2. Enemy Sprint 상태에서 충돌
    // 3. fire particle에 충돌
    public void OnHit()
    {
        print("적이 공격함");
        playerHealth.Instance.HP--;
    }
}
