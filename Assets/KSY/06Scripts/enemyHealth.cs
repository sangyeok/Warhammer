using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    Animator anim;
    CharacterController cc;

    public int hp = 3;
    
    private void Start()
    {
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
            print("enemy: " + hp);  
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
