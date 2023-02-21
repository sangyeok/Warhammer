using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// 적 구현 기능
// 1. Idle 및 이동
// 2. 플레이어 발견 시 이동
// 3. 플레이어에 가까이 다가가면 공격 후 5초 딜레이
// 4-1. 플레이어 한번 공격 시 날아감
// 4-2.

public class Enemy : MonoBehaviour
{
    Transform Target;
    public float speed = 0.01f;
    CharacterController cc = null;
    Animator anim;

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

        print(m_State);
    }

    float currentTime = 0;
    float moveRange = 7;

    // 거리가 가까워지면 
    private void Idle()
    {
        float distance = Vector3.Distance(Target.position, transform.position);
        if (distance < moveRange)
        {
            agent.enabled = true;
            m_State = EnemyState.Move;
            anim.SetTrigger("setMove");
        }

    }

    float attackRange = 1.5f;
    private void Move()
    {
        Vector3 dir = Target.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = Target.position;
        
        if(distance < attackRange)
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            anim.SetTrigger("setAttack");
        }
        if (distance > moveRange)
        {
            m_State = EnemyState.Idle;
            agent.enabled = false;
            anim.SetTrigger("setIdle");
        }

    }
    float attackDelaytime =5;

    private void Attack()
    {
        Vector3 dir = Target.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        currentTime += Time.deltaTime;
        if(currentTime > attackDelaytime)
        {
            currentTime = 0;
            anim.SetTrigger("setAttack");
        }

        float distance = Vector3.Distance(Target.transform.position, transform.position);
        if (distance > attackRange)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("setMove");
        }
    }

    // 언데드는 1, 살덩이는 5
    public int HP = 1;
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
            // Damage 애니메이션
            StartCoroutine(Damage());
        }
        else
        {
            m_State= EnemyState.Die;
            // Die 애니메이션
            cc.enabled= false;
            StartCoroutine(Die());
        }
    }

    public float damageDelayTime = 2;

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(damageDelayTime);
        m_State= EnemyState.Idle;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
