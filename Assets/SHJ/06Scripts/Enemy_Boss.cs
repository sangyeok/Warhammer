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

    public GameObject wall;
    

    public enum EnemyState
    {
        Delay,
        Idle,
        Move,
        Attack,
        Damage
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
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        m_State = EnemyState.Delay;

    }

    void Update()
    {

        switch (m_State)
        {
            case EnemyState.Delay:
                Delay();
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
        }

        //print(m_State);
    }

    // <보스 등장 방식>
    // 제자리 흔들거리기
    // 플레이어 감지하면(빛나는 돌 반경안에 들어오면)
    // 0. 플레이어 카메라 및 움직임 정지
    // 1. 카메라 전환
    // 2. 철장 올라감
    // 3. 보스 특정 지점으로 이동
    // 4. 카메라도 같이 이동
    // 5. 보스 으르렁
    // 6. 플레이어 다시 움직임 및 카메라 켜기

    private void Delay()
    {

        float distance = Vector3.Distance(Target.position, transform.position);
        if (distance < moveRange)
        {
            agent.enabled = true;
            m_State = EnemyState.Idle;
            anim.SetTrigger("setIdle");
        }
    }

    float currentTime = 0;
    public float moveRange = 50;

    // Idle 상태에서 일정시간이 지나면 이동으로 전환
    private void Idle()
    {
        wall.SetActive(true);
        //wall.GetComponent<BoxCollider>().enabled = true;
        currentTime += Time.deltaTime;
        if (currentTime > 2f)
        {
            currentTime = 0;
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
        currentTime += Time.deltaTime;
        // 이동하다 일정시간 후에 공격으로 전환
        if (currentTime > 5f)
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            currentTime = 0;
        }
    }

    public float attackDelaytime = 5;

    bool isAnim = false; // animation 중단문제 해결용

    private void Attack()
    {
        Vector3 dir = Target.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        // 3가지 공격 패턴
        int percent = Random.Range(0, 5);

        float distance = Vector3.Distance(Target.transform.position, transform.position);

        // 3f 범위 안에 있으면 공격
        if (distance < 3f)
        {
            // 펀치
            isAnim = true;
            anim.SetTrigger("Punch");
            StartCoroutine("PlayerTrack");
        }
        else if (percent <= 2 && distance > 3f)
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
        if (distance > attackRange && isAnim == false)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("setMove");
        }
    }

    bool isSprint = false;
    private void Fire()
    {
        // 파이어 공격
        // 손을 내미는 애니메이션
        isAnim = true;
        anim.SetTrigger("Fire");
        StartCoroutine("PlayerTrack");
    }

    public GameObject fireFactory;
    public GameObject firePos;

    void OnFire()
    {
        print("Fire");
        GameObject fire = Instantiate(fireFactory);
        fire.transform.position = firePos.transform.position;
        fire.transform.forward = firePos.transform.forward;
    }

    private void Sprint()
    {
        // 무기 내밀면서 달려들기
        // 애니메이션
        isAnim = true;
        anim.SetTrigger("Sprint");
        isSprint = true;
        StartCoroutine(Stay());
    }

    private IEnumerator PlayerTrack()
    {
        // 공격 후 2초 딜레이 후 Move
        yield return new WaitForSeconds(2);
        m_State = EnemyState.Move;
        agent.enabled = true;
        isAnim = false;
        anim.SetTrigger("setMove");
    }
    private IEnumerator Stay()
    {
        // Sprint 후 3초 딜레이 -> Idle 2초 딜레이 -> Move
        yield return new WaitForSeconds(3);
        m_State = EnemyState.Idle;
        anim.SetTrigger("setIdle");
    }

    // sprint 상태에서 충돌하면 player HP 감소
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && isSprint)
        {
            this.GetComponent<EnemyAttackEvent>().OnMyHit();
        }
    }
    // 언데드는 HP 2
    // 트롤은 HP 5
    // 보스는 HP 10
    public int enemyHp = 10;
    public void Damage()
    {
        if (enemyHp < 0)
        {
            enemyHp = 0;
            return;
        }
        StopAllCoroutines();
        agent.enabled = false;
        enemyHp--;
        if (enemyHp > 0)
        {
            
            print("enemyHp: " + enemyHp);
            anim.SetTrigger("Damage");
            StartCoroutine(OnDamage());
        }
        else if (enemyHp <= 0)
        {
            anim.SetTrigger("Die");
            cc.enabled = false;
        }
    }

    public float damageDelayTime = 2;
    private IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(damageDelayTime);
        m_State = EnemyState.Idle;
    }

}
