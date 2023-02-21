using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Ÿ�������� �̵��ϰ� �ʹ�.
// �ʿ�Ӽ� : �̵��ӵ�, Character Controller, Ÿ��
// FSM ����(����) �����

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
        // ��Ʈ�� �����϶��� �ƹ��͵� ���ϰ� �ϰ�ʹ�.
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

    // �����ð��� ������ ��븦 Move�� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ð�, ����ð�
    public float idleDelayTIme = 2;
    float currentTime = 0;
    private void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > idleDelayTIme)
        {
            currentTime = 0;
            m_State = EnemyState.Move;

            // �ִϸ��̼� ���µ� move�� ��ȯ�ϰ� �ʹ�
            anim.SetTrigger("Move");

            // ��ã�� Ȱ��ȭ
            agent.enabled = true;
        }
    }

    // Ÿ���� ���ݹ��� �ȿ� ������ ���¸� �������� ��ȯ�ϰ� �ʹ�.
    // �ʿ�Ӽ� : ���ݹ���
    // [HideInInspector]
    [SerializeField]
    public float attackRange = 2;
    private void Move()
    {
        // Ÿ�������� �̵��ϰ� �ʹ�.
        // 1. ������ �ʿ�
        Vector3 dir = target.transform.position - transform.position;
        float distance = dir.magnitude;
        agent.destination = target.position;
        /*dir.Normalize();
        dir.y = 0;
        // 2. �̵��ϰ� �ʹ�
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        cc.SimpleMove(dir * speed);*/
        // transform.position += dir * speed * Time.deltaTime;

        // 1. Ÿ�ٰ� ������ �Ÿ� ���
        // 2. Ÿ���� ���ݹ��� �ȿ� �������ϱ�.
        if (distance < attackRange)
        {
            //��ã�� ����
            agent.enabled = false;
            m_State = EnemyState.Attack;
            currentTime = attackDelayTIme;
            anim.SetTrigger("AttackStart");
        }
        // 3. ���¸� �������� ��ȭ�ϰ� �ʹ�.

    }

    // ����ڰ�  �ʿ��� ������(�����)�� �׸��� �ְ� ���ش�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // �����ð��� �ѹ��� �����ϰ� �ʹ�
    // �ʿ�Ӽ� : ���ݴ��ð�
    public float attackDelayTIme = 2;
    private void Attack()
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        currentTime += Time.deltaTime;
        if (currentTime > attackDelayTIme)
        {
            currentTime = 0;
            print("����!");
            anim.SetTrigger("Attack");
            // 3. Player�� �־���Ѵ�.
            // 2. PlayerHealth �� �־���Ѵ�.
            // playerHealth player = target.GetComponent<playerHealth>();
            // 1. ü�� -1 ����
            // player.SetHp(player.GetHp() - 1);
        }

        float distance = Vector3.Distance(target.position, transform.position);
        // Ÿ���� ���ݹ����� ����� ���¸� �̵����� ��ȯ
        if (distance > attackRange)
        {
            m_State = EnemyState.Move;
            agent.enabled = true;
            anim.SetTrigger("Move");
        }
    }

    // �ǰݴ������� �̺�Ʈ�Լ� ȣ��
    // ü���� ���������� ���¸� Damage�� ��ȯ
    // �׷��� ������ Die�� ��ȯ
    public float hp = 2;
    public void OnDamageProcess()
    {
        if (m_State == EnemyState.Die)
        {
            return;
        }
        // �ڷ�ƾ ������Ű��
        StopAllCoroutines();
        // ��ã�� ����
        agent.enabled = false;
        // hp1�����δ�
        hp--;
        // ���� hp�� 0�� �ƴϸ�
        if (hp > 0)
        {
            // Damage�� ��ȯ
            m_State = EnemyState.Damage;
            anim.SetTrigger("Damage");
            // �ڷ�ƾ����
            StartCoroutine(Damage());
        }
        // �׷��� ������
        else
        {
            // Die�� ��ȯ
            // ���� ���°� Die���
            m_State = EnemyState.Die;
            anim.SetTrigger("Die");
            StartCoroutine(Die());
            cc.enabled = false;
            // ���̻� ������� �ʰ� ����.
        }

    }

    public float damageDelayTime = 2;
    private IEnumerator Damage()
    {
        // �����ð� ��ٷȴٰ�
        yield return new WaitForSeconds(damageDelayTime);
        // ���¸� ���� ��ȯ�ϰ� �ʹ�.
        m_State = EnemyState.Idle;
    }

    // �Ʒ��� ����̵��ϸ� ��ġ�� -2���� ��������
    // ���ֹ�����
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

        // ���� ���� ������ ���� ������ 1�� �ø��� �ʹ�
    }
}
