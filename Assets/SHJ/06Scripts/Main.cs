using System.Collections;
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

    public GameObject warriorinfo;
    public GameObject warrior3d;
    public GameObject wizardinfo;
    public GameObject wizard3d;

    // Start is called before the first frame update
    void Start()
    {

        if (CharactorManager.instance.isWarrior == true)
        {
            // 메인 씬에서 어떤 캐릭터인지에 따라 캐릭터 설명 활성화
            warriorinfo.SetActive(true);
            warrior3d.SetActive(true);
            wizardinfo.SetActive(false);
            wizard3d.SetActive(false);

        }
        else if (CharactorManager.instance.isWizard == true)
        {
            warriorinfo.SetActive(false);
            warrior3d.SetActive(false);
            wizardinfo.SetActive(true);
            wizard3d.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("isWarrior:" + CharactorManager.instance.isWarrior);
        print("isWizard:" + CharactorManager.instance.isWizard);
    }
    public void charactorSelect()
    {
        SceneManager.LoadScene("Charactor");
    }

    public void setPlay()
    {
        // 마을 맵 씬 연결하기
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
