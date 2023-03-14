using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;


public class Video : MonoBehaviour
{
    public GameObject inputText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(InputText(time));
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.All))
        {
            SceneManager.LoadScene("Main");
        }
    }
    float time = 3f;

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
