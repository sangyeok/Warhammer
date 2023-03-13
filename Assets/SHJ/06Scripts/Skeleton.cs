using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// ���� �� 1.5�ʿ� �ϳ��� �÷��̾� ���󰡱�
// �÷��̾�� �浹������
// �÷��̾ �������� �ʴ´ٸ� �÷��̾� hp -1
// �÷��̾ �����Ѵٸ� �׳� ����
// �÷��̾�� �浹���ϸ� 3�� ������ ����

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
        // ���� �ð��� ������ ���ư���
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
