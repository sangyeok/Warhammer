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
            // ���� ���� ������ �ְ������� �Ѿ��
            if (curScore > highScore)
            {
                // �ְ������� ���������� ���ŵǰ� �ʹ�
                highScore = curScore;
                highUI.text = "Top Score : " + highScore;

                // �ְ����� ���� ��ġ
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
        // ���嵥���� �ҷ�����
        highScore = PlayerPrefs.GetInt("TopScore", highScore);
        // UI�� �ְ����� ǥ���ϱ�
        highUI.text = "Top Score : " + highScore;
    }
}
