using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    Enemy enemy;
    Animator anim;
    CharacterController cc;

    public int hp = 3;
    
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    public int HP
    {
        get
        {
            return hp;
        }
        set 
        {
            hp = value;
            enemy.m_State = Enemy.EnemyState.Damage;
            if (hp <= 0)
            {
                anim.SetTrigger("Die");
                cc.enabled= false;
            }
        }
    }

    /*private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
}
