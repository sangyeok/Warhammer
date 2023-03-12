using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

// 사용자의 입력에 따라 앞뒤 좌우로 이동하고 싶다.
// Character Controller 컴포넌트를 이용해 이동하게 하자.
// 중력을 적용받도록 하고 싶다.
// 필요속성 : 중력가속도, 수직속도
// 점프를 하고싶다.
// 필요속성 : 점프파워

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -20f;
    public float yVelocity = 0;
    public float jumpPower = 10f;
    CharacterController cc;
    Animator anim;
    public Transform body;
    public Transform hitCube;
    //public Transform bulletEffect;
    //ParticleSystem psBulletEffect;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //anim = GetComponentInChildren<Animator>();
        //anim.speed = 2;
        //psBulletEffect = bulletEffect.GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        if (GameManager.instance.IsIntro())
        {
            return;
        }
        // 등가속도 v = vo + at
        // transform.position += dir * speed * Time.deltaTime;
        
        yVelocity += gravity * Time.deltaTime;
        // 수직항력을 적용시켜줘야 한다.
        // 바닥에 있을 때는 수직속도를 0으로 해줘야 한다.
        // above, side, below
        //if (cc.isGrounded)
        //if (((int)cc.collisionFlags & (1 << (int)CollisionFlags.Below)) != 0)
        if (cc.collisionFlags  == CollisionFlags.Below)
        {
            //print("asd");
            yVelocity = 0;

        }

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();
        //anim.SetFloat("Speed", v);
        //anim.SetFloat("Direction", v * v * h);

        Vector3 bodydir = Camera.main.transform.forward;
        bodydir.y = 0;
        body.forward = bodydir;
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        //카메라 방향으로 몸체를 회전시키고 싶다.


        moveDir.y = yVelocity;
        cc.Move(moveDir * speed * Time.deltaTime);
        //if (Screen)
        //{
        //    Color c = Screen.color;
        //}
    }
    float h, v;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsIntro())
        {
            return;
        }


        //if (((cc.collisionFlags & CollisionFlags.CollidedBelow) != 0) && Input.GetButtonDown("Jump"))
        if (Input.GetButtonDown("Jump"))
        {
            print("cc.collisionFlags : " + cc.collisionFlags);
            //if (cc.collisionFlags == CollisionFlags.CollidedBelow)
            if ((cc.collisionFlags & CollisionFlags.CollidedBelow) != 0)
            {
                yVelocity = jumpPower;
            }

        }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //if (Input.GetMouseButtonDown(0))
        //{
        //    anim.SetTrigger("onweapon");
        //}
    }
    //public Image Screen;
    void OnHit()
    {
        Collider[] cols = Physics.OverlapBox(hitCube.position, hitCube.localScale * 0.5f);
        print("attack");
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Undead"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy>();
                CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
            else if (cols[i].CompareTag("Troll"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy_Mid>();
                CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
            else if (cols[i].CompareTag("Boss"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy_Boss>();
                CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
        }
    }
    //void ScreenUI()
    //{
    //    print("UI");
    //    //Screen.SetActive(true);
    //    //float curTime = 0;
    //    //float durTime = 3;
    //    //curTime += Time.time;
    //    //if(curTime> durTime)
    //    //{
    //    //    curTime = 0;
    //    //    Screen.SetActive(false);
    //    //}
    //}
}