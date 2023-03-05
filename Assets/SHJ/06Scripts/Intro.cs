using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject logo;
    public GameObject inputText;
    float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        StartCoroutine(Logo(time));
        if (currentTime > 5f && Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
    }

    float time = 3f;

    IEnumerator Logo(float time)
    {
    
        Image logoImage = logo.GetComponent<Image>();
        Color color = logoImage.color;
        color.a = 0f;
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            color.a = i / time;
            logoImage.color = color;
            yield return null;
        }

        color.a = 1f;
        logoImage.color = color;
        StartCoroutine(InputText(time));

    }


    IEnumerator InputText(float time)
    {
        TextMeshProUGUI text = inputText.GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        color.a = 0f;
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            color.a = i / time;
            text.color = color;
            yield return null;
        }

        color.a = 1f;
        text.color = color;

    }
}
