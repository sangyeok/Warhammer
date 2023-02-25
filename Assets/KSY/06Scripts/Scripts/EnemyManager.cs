using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    // 지정된 위치들에서 랜덤시간 간격에 한번씩 적을 만들고 싶다.
public class EnemyManager : MonoBehaviour
{
    // 필요속성 : 위치들, 랜덤시간 간격, 경과시간
    public float minTime = 2;
    public float maxTime = 5;
    float time = 0;
    public GameObject enemyFac;
    public Transform[] pos;
    float createTime;
    // Start is called before the first frame update
    void Start()
    {
        // 생성시간이 랜덤하게 구해져야 한다.
        createTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > createTime)
        {
            time = 0;
            GameObject enemy = Instantiate(enemyFac);
            enemy.transform.position = pos[Random.Range(0, pos.Length)].position;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
