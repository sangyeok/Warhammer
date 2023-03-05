using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharactorSelect : MonoBehaviour
{
    public GameObject warriorSet;
    public GameObject warrior3d;
    public GameObject wizardSet;
    public GameObject wizard3d;

    // Start is called before the first frame update
    void Start()
    {

        if (CharactorManager.instance.isWarrior == true)
        {
            // 메인 씬에서 어떤 캐릭터인지에 따라 캐릭터 설명 활성화
            warriorSet.SetActive(true); 
            warrior3d.SetActive(true);
            wizardSet.SetActive(false);
            wizard3d.SetActive(false);

        } else if(CharactorManager.instance.isWizard == true)
        {
            warriorSet.SetActive(false);
            warrior3d.SetActive(false);
            wizardSet.SetActive(true);
            wizard3d.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateWarrior()
    {
        CharactorManager.instance.isWarrior = true;
        if (!CharactorManager.instance.isWizard && CharactorManager.instance.isWarrior == true)
        {
            warriorSet.SetActive(true);
            warrior3d.SetActive(true);
            wizardSet.SetActive(false);
            wizard3d.SetActive(false);
        }
    }

    public void UpdateWizard()
    {
        CharactorManager.instance.isWizard = true;
        if (CharactorManager.instance.isWizard && !CharactorManager.instance.isWarrior)
        {
            warriorSet.SetActive(false);
            warrior3d.SetActive(false);
            wizardSet.SetActive(true);
            wizard3d.SetActive(true);
        }
    }

    public void setCharactor()
    {
        SceneManager.LoadScene("Main");
    }
}
