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
    bool isButton = false;

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
        if (isButton)
        {
            if (isWarrior && !isWizard)
            {
                SceneManager.LoadScene("MainWa");
            }
            else if (isWizard && !isWarrior)
            {
                SceneManager.LoadScene("MainWi");
            }
        }
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

    public void setMain()
    {
        isButton= true;
    }
}
