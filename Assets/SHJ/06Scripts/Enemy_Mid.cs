using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//최종


// 적 구현 기능
// 1. 기본 Idle 
// 2. 플레이어 발견 시 이동
// 3. 플레이어에 가까이 다가가면 공격 후 5초 딜레이
// 4. 플레이어가 공격하면 Damage

public class Enemy_Mid : MonoBehaviour
{
    Transform Target;
    public float speed = 1f;
    CharacterController cc = null;
    Animator anim;
    public Slider sliderHP;


    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    };

    public EnemyState m_State;
    NavMeshAgent agent;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Target = player.transform;
        }
        sliderHP.maxValue = enemyHp;
        HP = enemyHp;
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {

        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }

        //print(m_State);
    }

    float currentTime = 0;
    public float moveRange = 10;

    // Idle 상태로 있다가 플레이어와의 거리가 가까워지면 이동으로 전환
    private void Idle()
    {
        float distance = Vector3.Distance(Target.position, transform.position);
        if (distance < moveRange)
        {
            agent.enabled = true;
            m_State = EnemyState.Move;
            // anim.SetTrigger("setMove");
            anim.CrossFade("Move", 0.1f, 0);
        }
    }

    public float attackRange = 5f;
    // 타겟 방향으로 이동. 공격범위 안에 들어오면 공격으로 전환
    private void Move()
    {
        Vector3 dir = Target.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = Target.position;

        if (distance < attackRange)
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            currentTime = attackDelaytime;
        }
        if (distance > moveRange)
        {
            m_State = EnemyState.Idle;
            agent.enabled = false;
            //anim.SetTrigger("setIdle");
            anim.CrossFade("Idle", 0.1f, 0);
        }

    }
    int attackCount = 0;
    public float attackDelaytime = 5;
    // 일정시간에 한번씩 공격하기

    bool isAnim = false; // animation 중단문제 해결용

    private void Attack()
    {
        Vector3 dir = Target.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        currentTime += Time.deltaTime;
        if (currentTime > attackDelaytime)
        {
            currentTime = 0;
            attackCount++;
            if (attackCount < 2)
            {
                isAnim = true;
                agent.enabled = false;
                //anim.SetTrigger("setAttack");
                anim.CrossFade("Attack", 0.1f, 0);
                StartCoroutine("PlayerTrack");
            }
            else
            {
                isAnim = true;
                agent.enabled = false;
                //anim.SetTrigger("setJump");
                anim.CrossFade("Mutant Jump Attack", 0.1f, 0);
                attackCount = 0;
            }

        }

        float distance = Vector3.Distance(Target.transform.position, transform.position);
        // 공격범위를 벗어나면 이동으로 전환
        if (distance > attackRange && isAnim == false)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            //anim.SetTrigger("setMove");
            anim.CrossFade("Move", 0.1f, 0);
        }
    }


    private IEnumerator PlayerTrack()
    {
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        isAnim = false;
    }

    public int enemyHp = 5;

    int hp;
    int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderHP.value = hp;
        }
    }

    public void Damage()
    {
        if (HP < 0)
        {
            HP = 0;
            return;
        }
        StopAllCoroutines();
        agent.enabled = false;
        HP--;
        if (HP > 0)
        {
            m_State = EnemyState.Damage;
            //anim.SetTrigger("Damage");
            anim.CrossFade("Mutant Damage", 0.1f, 0);
            StartCoroutine(OnDamage());
        }
        else if (HP <= 0)
        {
            m_State = EnemyState.Die;
            //anim.SetTrigger("Die");
            anim.CrossFade("Mutant Die", 0.1f, 0);
            cc.enabled = false;
            agent.enabled = false;
        }
    }

    public float damageDelayTime = 2;

    // 일정시간 후 Idle 상태로 전환
    private IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(damageDelayTime);
        m_State = EnemyState.Idle;
    }

    //// 언데드는 HP 1
    //// 트롤은 HP 3
    //public int HP = 3;
    //public void onDamageProcess()
    //{
    //    if(m_State== EnemyState.Die) 
    //    {
    //        return;
    //    }
    //    StopAllCoroutines();
    //    agent.enabled = false;
    //    currentTime= 0;
    //    HP--;
    //    if(HP > 0)
    //    {
    //        m_State = EnemyState.Damage;
    //        anim.SetTrigger("Damage");
    //        StartCoroutine(Damage());
    //    }
    //    else
    //    {
    //        m_State= EnemyState.Die;
    //        anim.SetTrigger("Die");
    //        cc.enabled= false;
    //    }
    //}


}
