using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ������� �Է¿� ���� �յ� �¿�� �̵��ϰ� �ʹ�.
// Character Controller ������Ʈ�� �̿��� �̵��ϰ� ����.
// �߷��� ����޵��� �ϰ� �ʹ�.
// �ʿ�Ӽ� : �߷°��ӵ�, �����ӵ�
// ������ �ϰ�ʹ�.
// �ʿ�Ӽ� : �����Ŀ�

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -20f;
    float yVelocity = 0;
    public float jumpPower = 10f;
    CharacterController cc;
    Animator anim;
    public Transform body;
    bool isinAir = false;
    public Transform hitCube;
    public Transform bulletEffect;
    //ParticleSystem psBulletEffect;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        anim.speed = 2;
        //psBulletEffect = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsIntro())
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();
        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", v * v * h);

        Vector3 bodydir = Camera.main.transform.forward;
        bodydir.y = 0;
        body.forward = bodydir;
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        //ī�޶� �������� ��ü�� ȸ����Ű�� �ʹ�.

        // ��ӵ� v = vo + at
        // transform.position += dir * speed * Time.deltaTime;
        yVelocity += gravity * Time.deltaTime;
        // �����׷��� ���������� �Ѵ�.
        // �ٴڿ� ���� ���� �����ӵ��� 0���� ����� �Ѵ�.
        // above, side, below
        // if(cc.isGrounded)
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            isinAir = false;
        }
        if (isinAir == false && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            isinAir = true;
        }
        moveDir.y = yVelocity;
        cc.Move(moveDir * speed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("onweapon");
        }
        if(Screen)
        {
            Color c = Screen.color;
        }
    }
    public Image Screen;
    void OnHit()
    {
        Collider[] cols = Physics.OverlapBox(hitCube.position, hitCube.localScale * 0.5f);
        for (int i = 0; i < cols.Length; i++)
        {
            var enemyHP = cols[i].GetComponentInChildren<enemyHealth>();
            if (enemyHP)
            {
                //ScreenUI();
                CamShakeManager.Instance.Play();
                //bulletEffect.position = hitCube.position;
                //bulletEffect.forward = hitCube.forward;
                //psBulletEffect.Stop();
                //psBulletEffect.Play();
                if (enemyHP.HP != 0)
                {
                    print("min hp");
                    enemyHP.HP--;
                }
            }
        }
    }



    //void ScreenUI()
    //{
    //    //print("UI");
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