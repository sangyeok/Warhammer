using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

// 적 구현 기능
// 1. 기본 Idle 
// 2. 플레이어 발견 시 이동
// 3. 플레이어에 가까이 다가가면 공격 후 5초 딜레이
// 4. 플레이어가 공격하면 Damage

public class Enemy_Boss : MonoBehaviour
{
    Transform Target;
    public float speed = 1f;
    CharacterController cc = null;
    Animator anim;

    public static Enemy_Boss instance;

    public enum EnemyState
    {
        Appear,
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
        if(player != null)
        {
            Target= player.transform;
        }
        cc = GetComponent<CharacterController>();
        agent= GetComponent<NavMeshAgent>();
        anim= GetComponent<Animator>();

    }

    void Update()
    {
        
        switch (m_State)
        {
            case EnemyState.Appear:
                Appear();
                break;
            case EnemyState.Idle:
                Idle();
                break; 
            case EnemyState.Move:
                Move();
                break; 
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damage:
                break;
            case EnemyState.Die:
                break;
        }

        //print(m_State);
    }

    private void Appear()
    {
        throw new NotImplementedException();
    }

    float currentTime = 0;
    public float moveRange = 10;

    // Idle 상태에서 일정시간이 지나면 이동으로 전환
    private void Idle()
    {
        currentTime += Time.deltaTime;
        if(currentTime > 2f)
        {
            currentTime= 0;
            m_State = EnemyState.Move;
            anim.SetTrigger("setMove");

            agent.enabled = true;
        }
    }

    public float attackRange = 3f;
    // 타겟 방향으로 이동. 일정 시간 후에 공격
    private void Move()
    {
        Vector3 dir = Target.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = Target.position;
        currentTime+= Time.deltaTime;
        // 이동하다 일정시간 후에 공격으로 전환
        if(currentTime > 5f)
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            currentTime= 0;
        }
    }

    public float attackDelaytime =5;

    private void Attack()
    {
        Vector3 dir = Target.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        // 3가지 공격 패턴
        int percent = Random.Range(0,5);

        float distance = Vector3.Distance(Target.transform.position, transform.position);

        // 3f 범위 안에 있으면 공격
        if (distance < 4f)
        {
            // 펀치
            anim.SetTrigger("Punch");
            StartCoroutine(Follow());
        }
        else if( percent <= 2 && distance > 3f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
            Fire();
        }
        else if (percent <= 5 && distance > 3f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
            Sprint();
        }

        // 공격범위를 벗어나면 이동으로 전환
        if (distance > attackRange)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("setMove");
        }
    }

    bool isAttack = false;
    private void Fire()
    {

        // 파이어 공격
        // 손을 내미는 애니메이션
        anim.SetTrigger("Fire");
        isAttack = true;
        // 파티클 파이어 쏘기

        StartCoroutine(Follow());
    }

    private void Sprint()
    {
        // 무기 내밀면서 달려들기
        // 애니메이션
        anim.SetTrigger("Sprint");
        isAttack = true;

        StartCoroutine(Stay());
    }

    private IEnumerator Follow()
    {
        // 공격 후 2초 딜레이 후 Move
        yield return new WaitForSeconds(2);
        m_State = EnemyState.Move;
        agent.enabled = true;
        anim.SetTrigger("setMove");
    }
    private IEnumerator Stay()
    {
        // Sprint 후 3초 딜레이 -> Idle 2초 딜레이 -> Move
        yield return new WaitForSeconds(3);
        m_State = EnemyState.Idle;
        anim.SetTrigger("setIdle");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && isAttack)
        {
            this.GetComponent<EnemyAttackEvent>().OnHit();
        }
    }
    // 언데드는 HP 1
    // 트롤은 HP 3
    public int HP = 3;
    public void onDamageProcess()
    {
        if(m_State== EnemyState.Die) 
        {
            return;
        }
        StopAllCoroutines();
        agent.enabled = false;
        currentTime= 0;
        HP--;
        if(HP > 0)
        {
            m_State = EnemyState.Damage;
            anim.SetTrigger("Damage");
            StartCoroutine(Damage());
        }
        else
        {
            m_State= EnemyState.Die;
            anim.SetTrigger("Die");
            cc.enabled= false;
        }
    }

    public float damageDelayTime = 2;

    // 일정시간 후 Idle 상태로 전환
    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(damageDelayTime);
        m_State= EnemyState.Idle;
    }

}
