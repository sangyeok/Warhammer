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
        // ��ź�߻� ����� ���� �Ѿ� ��������
        if(bGrenade)
        {
            // �Ѿ� ��������
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
        // ��ź�߻� Ȱ��ȭ�Ǹ�
        if (bGrenade)
        {
            // �Ѿ� ��������
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
        // 1. �߻��ư�� �������ϱ�
        if (Input.GetButtonDown("Fire1"))
        {
            // �ִϸ��̼� ó��
            //anim.SetTrigger("Attack");
            anim.CrossFade("Attack", 0.2f, 1 ,0.5f);

            // 2. RayCastHit �׸� �ʿ�
            RaycastHit hitinfo = new RaycastHit();
            // 3. Ray �ʿ�
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 4. �Ѿ˹߻�
            // - �ڱ��ڽ��� �浹 ����
            //int layer = LayerMask.NameToLayer("Player");
            int layer = 1 << gameObject.layer;
            bool bHit = Physics.Raycast(ray, out hitinfo, 500, ~layer);

            // 4. �Ѿ˹߻�
            if (bHit)
            {
                // �ε��� �༮�� enemy���
                // �ε��� �༮���� enemy ������Ʈ �޶�� ����
                // ���� ���������� ������
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
                    // -> �¾Ҵٰ� �˷��ְ� �ʹ�.
                    print("222222222");
                }*/
                bulletEffect.position = hitinfo.point;
                // effect �� ���ϴ� ������ normal �������� ��������
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
        // ����ڰ� �߻� ��ư�� ������
        // �Ѿ��� �־���Ѵ�
        // �Ѿ˹߻�(��ġ)
        fireRate += Time.deltaTime;
        if (fireRate > 0.5)
        {
            // źâ�� �Ѿ��� ���� ���� �߻��ϰ� �ʹ�.
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
        // źâ�� �Ѿ��� �ϳ��� ���� �ְ� �ʹ�.
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bulletPool.Add(bullet);
            // �Ѿ��� ��Ȱ��ȭ ��Ų��.
            bullet.SetActive(false);
        }
    }
}
