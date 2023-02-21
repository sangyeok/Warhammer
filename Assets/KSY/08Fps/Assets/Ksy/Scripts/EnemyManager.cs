using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    // ������ ��ġ�鿡�� �����ð� ���ݿ� �ѹ��� ���� ����� �ʹ�.
public class EnemyManager : MonoBehaviour
{
    // �ʿ�Ӽ� : ��ġ��, �����ð� ����, ����ð�
    public float minTime = 2;
    public float maxTime = 5;
    float time = 0;
    public GameObject enemyFac;
    public Transform[] pos;
    float createTime;
    // Start is called before the first frame update
    void Start()
    {
        // �����ð��� �����ϰ� �������� �Ѵ�.
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
