using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적이 공격할 때 이벤트가 호출되면 처리할 클래스

public class EnemyHitEvent : MonoBehaviour
{
    void OnHit()
    {
        playerHealth.Instance.HP--;
        //playerHealth.Instance.SetHp(playerHealth.Instance.GetHp() - 1);
    }
}
