using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڽ��� ���ϴ� �������� �߻�ǰ� �ʹ�.
// �ʿ�Ӽ� : �߻�ӵ�
public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float lifeTime = 2;
    float life = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        // �ڽ��� ���ϴ� �������� �߻�ǰ� �ʹ�.
        // rigidbody�� �̿��� �߻��ϱ�
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life >= lifeTime)
        {
            life = 0;
            gameObject.SetActive(false);
            // 2.player GameObject
            GameObject player = GameObject.Find("Player");
            // 1.playerFire ������Ʈ
            PlayerFire pf = player.GetComponent<PlayerFire>();
            // 3.źâ�ȿ� �־�����Ѵ�;
            pf.bulletPool.Add(gameObject);
        }
    }
}
