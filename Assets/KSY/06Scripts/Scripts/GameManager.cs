using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���¸� �����ϴ� ��ü
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Intro,
        Playing,
        GameOver
    }

    public GameState m_state;
    public static GameManager instance;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsIntro()
    {
        return m_state == GameState.Intro;
    }
}
