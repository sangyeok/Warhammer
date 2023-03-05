using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorManager : MonoBehaviour
{
    public static CharactorManager instance;


    public bool isWarrior = false;
    public bool isWizard = false;

    // Start is called before the first frame update
    void Start()
    {
        
        isWarrior = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWarrior)
        {
            isWarrior = true;
            isWizard = false;
        }
        else if (isWizard)
        {
            isWarrior = false;
            isWizard = true;
        }
    }
}
