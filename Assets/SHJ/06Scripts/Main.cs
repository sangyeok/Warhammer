﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // 선택된 캐릭터를 기준으로 설명 변경
    // Play -> 마을맵으로 이동
    // Options -> 옵션 창
    // Prologue -> 영상 재생
    // Quit -> 게임 종료

    public bool isWarrior = false;
    public bool isWizard = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCharactor()
    {
        SceneManager.LoadScene("Charactor");
    }

    public void setPlay()
    {
        SceneManager.LoadScene("Level");

        
    }     
    
    public void setPrologue()
    {
        SceneManager.LoadScene("Prologue");
    }    
    
    public void setQuit()
    {
        Application.Quit();
    }
}
