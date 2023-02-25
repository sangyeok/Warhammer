using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeManager : MonoBehaviour
{
    public enum EShakeType
    {
        Random,
        Sine,
        Anim
    }

    public static CamShakeManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Play(EShakeType shakeType = EShakeType.Random)
    {
        switch (shakeType)
        {
            case EShakeType.Random:
                GetComponent<CamShakeRandom>().OnStartShake();
                break;
            case EShakeType.Sine:
                break;
            case EShakeType.Anim:
                break;
        }
    }
}
