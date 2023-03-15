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

    public bool isWarrior = false;
    public bool isWizard = false;

    // Start is called before the first frame update
    void Start()
    {

        warriorSet.SetActive(true);
        warrior3d.SetActive(true);
        wizardSet.SetActive(false);
        wizard3d.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateWarrior()
    {
        isWarrior = true;
        if (isWarrior)
        {
            isWizard = false;
            warriorSet.SetActive(true);
            warrior3d.SetActive(true);
            wizardSet.SetActive(false);
            wizard3d.SetActive(false);
        }
    }

    public void UpdateWizard()
    {
        isWizard = true;
        if (isWizard)
        {
            isWarrior = false;
            warriorSet.SetActive(false);
            warrior3d.SetActive(false);
            wizardSet.SetActive(true);
            wizard3d.SetActive(true);
        }
    }

    public void choiceWarrior()
    {
        isWarrior = true;
        isWizard = false;
    }
    public void choiceWizard()
    {
        isWizard = true;
        isWarrior = false;
    }

    public void setMain()
    {
        if (isWarrior && !isWizard)
        {
            isWarrior = true;
            SceneManager.LoadScene("MainWa");
        }
        else if (!isWarrior && isWizard)
        {
            isWizard = true;
            SceneManager.LoadScene("MainWi");
        }
    }

}
