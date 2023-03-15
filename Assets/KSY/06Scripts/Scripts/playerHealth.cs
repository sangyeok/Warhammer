using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 최종
public class playerHealth : MonoBehaviour
{
    public static int hp = 10;
    public static playerHealth Instance;

    public int HP
    { 
        
        get
        {
            return hp;
        }
        set 
        {
            
            //StartCoroutine(UI());
            hp = value;
            print("Player :" + hp);
            if (hp <= 0)
            {
                SceneManager.LoadScene("Main");
            }

        }

    }

    private void Awake()
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
        //hpbar.enabled = false;
    }

    //public Image hpbar;
    /*public int GetHp()
    {
        return hp;
    }
    // hp 값을 설정하는 기능
    public void SetHp(int value)
    {
        StartCoroutine(UI());
        hp = value;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }*/
    //private IEnumerator UI()
    //{
    //    hpbar.enabled = true;
    //    yield return new WaitForSeconds(0.3f);
    //    hpbar.enabled = false;
    //}

}
