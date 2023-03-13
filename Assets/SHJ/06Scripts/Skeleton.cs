using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// 생성 후 1.5초에 하나씩 플레이어 따라가기
// 플레이어랑 충돌했을때
// 플레이어가 공격하지 않는다면 플레이어 hp -1
// 플레이어가 공격한다면 그냥 삭제
// 플레이어랑 충돌안하면 3초 지나고 삭제

public class Skeleton : MonoBehaviour
{
    public float currentTime = 0;
    public float Speed = 7f;
    Transform Target;
    Vector3 dir;
    float createTime;
    public float minTime = 1;
    public float maxTime = 4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 랜덤 시간이 지나면 날아가기
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            dir = Target.position - transform.position;
            dir.Normalize();
            transform.position += dir * Speed * Time.deltaTime;
            transform.forward = dir;
            createTime = Random.Range(minTime, maxTime);
        }

        Destroy(gameObject, 8);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.Instance.HP--;
            Destroy(gameObject);
        }
    }
}
