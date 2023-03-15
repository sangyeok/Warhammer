using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

// ����

// ������� �Է¿� ���� �յ� �¿�� �̵��ϰ� �ʹ�.
// Character Controller ������Ʈ�� �̿��� �̵��ϰ� ����.
// �߷��� ����޵��� �ϰ� �ʹ�.
// �ʿ�Ӽ� : �߷°��ӵ�, �����ӵ�
// ������ �ϰ�ʹ�.
// �ʿ�Ӽ� : �����Ŀ�

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;
    public Transform foot;
    private void Awake()
    {
        Instance = this;
    }


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
        //transform.position = new Vector3(-114.5f, -117f, 45f);
    }

    void LateUpdate()
    {
        if (GameManager.instance.IsIntro())
        {
            return;
        }
        // ��ӵ� v = vo + at
        // transform.position += dir * speed * Time.deltaTime;
        
        yVelocity += gravity * Time.deltaTime;
        // �����׷��� ���������� �Ѵ�.
        // �ٴڿ� ���� ���� �����ӵ��� 0���� ����� �Ѵ�.
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
        //ī�޶� �������� ��ü�� ȸ����Ű�� �ʹ�.


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
    
}