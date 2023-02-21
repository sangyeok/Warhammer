using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// 타겟쪽으로 이동하고 싶다.
// 필요속성 : 이동속도, Character Controller, 타겟
// FSM 뼈대(목차) 만들기

public class Enemy : MonoBehaviour
{
    CharacterController cc;
    Transform target;
    public float speed = 4;

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }

    public EnemyState m_State;
    // Start is called before the first frame update
    NavMeshAgent agent;
    Animator anim;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            target = player.transform;
        }
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 인트로 상태일때는 아무것도 못하게 하고싶다.
        if (GameManager.instance.IsIntro())
        {
            return;
        }
        print("State : " + m_State);
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
                Damage();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    // 일정시간이 지나면 상대를 Move로 전환하고 싶다.
    // 필요속성 : 대기시간, 경과시간
    public float idleDelayTIme = 2;
    float currentTime = 0;
    private void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > idleDelayTIme)
        {
            currentTime = 0;
            m_State = EnemyState.Move;

            // 애니메이션 상태도 move로 전환하고 싶다
            anim.SetTrigger("Move");

            // 길찾기 활성화
            agent.enabled = true;
        }
    }

    // 타겟이 공격범위 안에 들어오면 상태를 공격으로 전환하고 싶다.
    // 필요속성 : 공격범위
    // [HideInInspector]
    [SerializeField]
    public float attackRange = 2;
    private void Move()
    {
        // 타겟쪽으로 이동하고 싶다.
        // 1. 방향이 필요
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = target.position;
        /*dir.Normalize();
        dir.y = 0;
        // 2. 이동하고 싶다
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        cc.SimpleMove(dir * speed);*/
        // transform.position += dir * speed * Time.deltaTime;

        // 1. 타겟과 나와의 거리 계산
        // 2. 타겟이 공격범위 안에 들어왔으니까.
        if (distance < attackRange)
        {
            //길찾기 중지
            agent.enabled = false;
            m_State = EnemyState.Attack;
            currentTime = attackDelayTIme;
            anim.SetTrigger("AttackStart");
        }
        // 3. 상태를 공격으로 전화하고 싶다.

    }

    // 사용자가  필요한 아이콘(기즈모)를 그릴수 있게 해준다
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // 일정시간에 한번씩 공격하고 싶다
    // 필요속성 : 공격대기시간
    public float attackDelayTIme = 2;
    private void Attack()
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        currentTime += Time.deltaTime;
        if (currentTime > attackDelayTIme)
        {
            currentTime = 0;
            print("공격!");
            anim.SetTrigger("Attack");
            // 3. Player가 있어야한다.
            // 2. PlayerHealth 가 있어야한다.
            // playerHealth player = target.GetComponent<playerHealth>();
            // 1. 체력 -1 감소
            // player.SetHp(player.GetHp() - 1);
        }

        float distance = Vector3.Distance(target.position, transform.position);
        // 타겟이 공격범위를 벗어나면 상태를 이동으로 전환
        if (distance > attackRange)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("Move");
        }
    }

    // 피격당했을때 이벤트함수 호출
    // 체력이 남아있으면 상태를 Damage로 전환
    // 그렇지 않으면 Die로 전환
    public float hp = 2;
    public void OnDamageProcess()
    {
        if (m_State == EnemyState.Die)
        {
            return;
        }
        // 코루틴 중지시키자
        StopAllCoroutines();
        // 길찾기 중지
        agent.enabled = false;
        // hp1씩깎인다
        hp--;
        // 만약 hp가 0이 아니면
        if (hp > 0)
        {
            // Damage로 전환
            m_State = EnemyState.Damage;
            anim.SetTrigger("Damage");
            // 코루틴실행
            StartCoroutine(Damage());
        }
        // 그렇지 않으면
        else
        {
            // Die로 전환
            // 만약 상태가 Die라면
            m_State = EnemyState.Die;
            anim.SetTrigger("Die");
            StartCoroutine(Die());
            cc.enabled = false;
            // 더이상 진행되지 않게 하자.
        }

    }

    public float damageDelayTime = 2;
    private IEnumerator Damage()
    {
        // 일정시간 기다렸다가
        yield return new WaitForSeconds(damageDelayTime);
        // 상태를 대기로 전환하고 싶다.
        m_State = EnemyState.Idle;
    }

    // 아래로 등속이동하며 위치가 -2미터 내려가면
    // 없애버리자
    public float dieSpeed = 0.5f;
    float time = 0f;
    private IEnumerator Die()
    {
        ScoreManager.Instance.CurScore++;
        yield return new WaitForSeconds(2);
        while (transform.position.y < 2)
        {
            transform.position += Vector3.up * dieSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);

        // 적을 잡을 때마다 현재 점수를 1씩 올리고 싶다
    }
}
