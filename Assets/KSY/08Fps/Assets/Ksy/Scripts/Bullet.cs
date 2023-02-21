using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자신이 향하는 방향으로 발사되고 싶다.
// 필요속성 : 발사속도
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
        // 자신이 향하는 방향으로 발사되고 싶다.
        // rigidbody를 이용해 발사하기
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
            // 1.playerFire 컴포넌트
            PlayerFire pf = player.GetComponent<PlayerFire>();
            // 3.탄창안에 넣어줘야한다;
            pf.bulletPool.Add(gameObject);
        }
    }
}
