using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject main;
    public GameObject chara;

    public bool isMain = false;
    public bool isCharactor = false;

    public bool isWarrior = false;
    public bool isWizard = false;

    // Start is called before the first frame update
    void Start()
    {
        
        isMain= true;
        isWarrior= true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMain)
        {
            UpdateMain();

        }
        else if(isCharactor)
        {
            UpdateCharactor();

        }

    }

    private void UpdateMain()
    {
        main.SetActive(true);
        chara.SetActive(false);
    }

    private void UpdateCharactor()
    {
        main.SetActive(false);
        chara.SetActive(true);
    }

    // 버튼에서 가져오기
    public void seteMain()
    {
        isMain = true;
    }

    public void setCharactor()
    {
        isCharactor= true;
    }
}
