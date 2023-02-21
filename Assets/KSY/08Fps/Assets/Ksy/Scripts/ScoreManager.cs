using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int curScore;
    private int highScore;
    public Text ScoreUI;
    public Text highUI;
    // Start is called before the first frame update

    public int CurScore
    {
        get
        {
            return curScore;
        }
        set
        {
            curScore = value;

            ScoreUI.text = "Score : " + curScore;
            // 만약 현재 점수가 최고점수를 넘어서면
            if (curScore > highScore)
            {
                // 최고점수는 현재점수로 갱신되고 싶다
                highScore = curScore;
                highUI.text = "Top Score : " + highScore;

                // 최고점수 저장 위치
                PlayerPrefs.SetInt("TopScore", highScore);
            }
        }
    }

    public static ScoreManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    private void Start()
    {
        // 저장데이터 불러오기
        highScore = PlayerPrefs.GetInt("TopScore", highScore);
        // UI에 최고점수 표시하기
        highUI.text = "Top Score : " + highScore;
    }
}
