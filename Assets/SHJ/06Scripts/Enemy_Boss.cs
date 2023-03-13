using Oculus.Platform.Samples.VrHoops;
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

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public enum EnemyState
    {
        Delay,
        Appear,
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

    public GameObject undeadFactory;
    public GameObject[] undeadPos;
    float makeTime = 0;

    void Update()
    {

        switch (m_State)
        {
            case EnemyState.Delay:
                break;
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
        }

        print(m_State);

        makeTime += Time.deltaTime;
        if (makeTime > 10)
        {
            GameObject undead = Instantiate(undeadFactory);
            int undeadCount = Random.Range(0, undeadPos.Length - 1);
            undead.transform.position = undeadPos[undeadCount].transform.position;
            undead.transform.forward = undeadPos[undeadCount].transform.forward;
            makeTime = 0;
        }

    }

    // <보스 등장 방식>
    // 제자리 흔들거리기 : delay
    // 플레이어 감지하면(빛나는 돌 반경안에 들어오면)
    // 0. 플레이어 카메라 및 움직임 정지 v
    // 1. 카메라 전환 v
    // 2. 철장 올라감 v
    // 3. 보스 특정 지점으로 이동 : void Appear() v
    // 4. 카메라도 같이 이동 -> boss 안에 카메라 넣기 v
    // 5. 보스 으르렁 v
    // 6. 플레이어 다시 움직임 및 카메라 끄기 v

    bool isColse = true;
    public Transform appearPos;
    public void Appear()
    {
        wall.SetActive(true);
        //wall.GetComponent<BoxCollider>().enabled = true;

        // 적이 등장 위치로 이동
        Vector3 dir = appearPos.position - transform.position;
        dir.y = 0;
        float distance = dir.magnitude;
        agent.destination = appearPos.position;
        float dis = Vector3.Distance(transform.position, appearPos.position);
        // 이동하는 동안 걷기 애니메이션
        anim.SetFloat("Chase", dis); // 거리를 계산해서 1보다 크면 뒤쫓기
        if (dis < 1 && isColse)
        {
            // Pos에 도착하면 울부짖기
            anim.SetTrigger("Roar");
            StartCoroutine("CameraChange");
        }
    }
    public new GameObject camera;

    IEnumerator CameraChange()
    {
        // 울부짖기 시작 2초 후 (코루틴)
        yield return new WaitForSeconds(2f);

        // 1. 카메라 끄기
        //camera.SetActive(false);
        Destroy(camera);
        // 2. 플레이어 스크립트 켜기
        Target.GetComponent<PlayerMove>().enabled = true;
        // 3. 적 아이들 상태로 전환
        m_State = EnemyState.Idle;
    }

    float currentTime = 0;
    public float moveRange = 50;

    // Idle 상태에서 일정시간이 지나면 이동으로 전환
    private void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2f)
        {
            currentTime = 0;
            m_State = EnemyState.Move;
            anim.SetTrigger("setMove");

            agent.enabled = true;
        }
    }

    public float attackRange = 10f;
    // 타겟 방향으로 이동. 일정 시간 후에 공격
    private void Move()
    {
        Vector3 dir = Target.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = Target.position;
        currentTime += Time.deltaTime;
        // 이동하다 일정시간 후에 공격으로 전환
        if (currentTime > 1f)
        {
            agent.enabled = true;
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
        // 2가지 공격 패턴
        // 1. 가까이 있으면 때리기
        // 2. 랜덤 확률로 포도알 생성
        // 3. 포도알이 플레이어 따라가기

        float distance = Vector3.Distance(Target.transform.position, transform.position);

        if (distance < 4f) // 4f 범위 안에 있으면 펀치
        {
            currentTime += Time.deltaTime;
            if (currentTime > 1)
            {
                currentTime = 0;
                isAnim = true;
                agent.enabled = false;
                anim.SetTrigger("Punch");
                StartCoroutine("PlayerTrack");
            }
        }
        else if (distance > 5f && isAnim == false) // 5f 범위 밖에 있으면 해골 날리기
        {
            currentTime += Time.deltaTime;
            if (currentTime > 5)
            {
                currentTime = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
                isExistence = true;
                Fire();
            }
        }
        // 공격범위를 벗어나면 이동으로 전환
        else if (distance > attackRange && isAnim == false)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("setMove");
        }
    }

    public GameObject skeletonFactory;
    public GameObject[] skeletonPos;
    bool isExistence = false;

    private void Fire()
    {
        anim.SetTrigger("Roar02");
        // agent 중지
        agent.enabled = false;
        // Pos 4개에 해골 동시 생성
        isAnim = true;
        if (isExistence)
        {
            GameObject skeleton = Instantiate(skeletonFactory);
            int EnemyCount = Random.Range(0, skeletonPos.Length - 1);
            skeleton.transform.position = skeletonPos[EnemyCount].transform.position;
            skeleton.transform.forward = skeletonPos[EnemyCount].transform.forward;
            isExistence = false;
        }
        StartCoroutine("PlayerTrack02");
    }

    private IEnumerator PlayerTrack()
    {
        // 공격 후 1초 딜레이 후 Move
        yield return new WaitForSeconds(0.5f);
        m_State = EnemyState.Move;
        agent.enabled = true;
        isAnim = false;
        anim.SetTrigger("setMove");
    }

    private IEnumerator PlayerTrack02()
    {
        // 공격 후 2초 딜레이 후 Move
        yield return new WaitForSeconds(5);
        m_State = EnemyState.Move;
        agent.enabled = true;
        isAnim = false;
        anim.SetTrigger("setMove");
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
