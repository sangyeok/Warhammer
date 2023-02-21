using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePosition;
    //Vector3 dir;
    public int bulletPoolSize = 10;
    // GameObject[] bulletPool; $$
    public List<GameObject> bulletPool;
    public bool bGrenade = false;
    public Transform bulletEffect;
    ParticleSystem psBulletEffect;
    AudioSource asBulletSound;
    public float fireRate = 0.5f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
        // 유탄발사 사용할 때만 총알 만들어놓자
        if(bGrenade)
        {
            // 총알 만들어놓자
            MakeGrenade();
        }
        else
        {
            psBulletEffect = bulletEffect.GetComponent<ParticleSystem>();
            asBulletSound = bulletEffect.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UseSniper();
        if (GameManager.instance.IsIntro())
        {
            return;
        }
        // 유탄발사 활성화되면
        if (bGrenade)
        {
            // 총알 만들어놓자
            FireGrenade();
            return;
        }
        FireRay();
    }
    public GameObject sni;
    private void UseSniper()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = 30;
            sni.SetActive(true);

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = 60;
            sni.SetActive(false);
        }
    }

    void FireRay()
    {
        // 1. 발사버튼을 눌렀으니까
        if (Input.GetButtonDown("Fire1"))
        {
            // 애니메이션 처리
            //anim.SetTrigger("Attack");
            anim.CrossFade("Attack", 0.2f, 1 ,0.5f);

            // 2. RayCastHit 그릇 필요
            RaycastHit hitinfo = new RaycastHit();
            // 3. Ray 필요
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 4. 총알발사
            // - 자기자신은 충돌 제외
            //int layer = LayerMask.NameToLayer("Player");
            int layer = 1 << gameObject.layer;
            bool bHit = Physics.Raycast(ray, out hitinfo, 500, ~layer);

            // 4. 총알발사
            if (bHit)
            {
                // 부딪힌 녀석이 enemy라면
                // 부딪힌 녀석한테 enemy 컴포넌트 달라고 하자
                // 만약 정상적으로 얻어오면
                // -> Enemy
                Enemy enemy = hitinfo.transform.GetComponent<Enemy>();
                if(enemy)
                {
                    enemy.OnDamageProcess();
                }
                /*string name = hitinfo.transform.name;
                    print("1111");
                if (name.Contains("Enemy"))
                {
                    // -> 맞았다고 알려주고 싶다.
                    print("222222222");
                }*/
                bulletEffect.position = hitinfo.point;
                // effect 가 향하는 방향을 normal 방향으로 맞춰주자
                bulletEffect.forward = hitinfo.normal;
                psBulletEffect.Stop();
                psBulletEffect.Play();
                asBulletSound.Stop();
                asBulletSound.Play();
            }
        }
    }

    private void FireGrenade()
    {
        // 사용자가 발사 버튼을 누르면
        // 총알이 있어야한다
        // 총알발사(위치)
        fireRate += Time.deltaTime;
        if (fireRate > 0.5)
        {
            // 탄창에 총알이 있을 때만 발사하고 싶다.
            if (bulletPool.Count > 0)
            {
                GameObject bullet = bulletPool[0];
                bullet.transform.position = firePosition.position;
                bullet.transform.forward = firePosition.forward;
                bullet.SetActive(true);
                /*for (int i = 0; i <= bulletPoolSize; i++) 
                {
                    GameObject bullet = bulletPool[i];
                    if (bullet.activeSelf == false)
                    {
                        bullet.transform.position = firePosition.position;
                        bullet.transform.forward = firePosition.forward;
                        bullet.SetActive(true);
                        break;
                    }
                }*/
                bulletPool.RemoveAt(0);
            }
            fireRate = 0;
        }
    }

    private void MakeGrenade()
    {
        bulletPool = new List<GameObject>();
        // 탄창에 총알을 하나씩 만들어서 넣고 싶다.
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bulletPool.Add(bullet);
            // 총알을 비활성화 시킨다.
            bullet.SetActive(false);
        }
    }
}
