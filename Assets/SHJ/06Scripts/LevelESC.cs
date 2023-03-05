using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelESC : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        menu.SetActive(false);
    }

    public void Hero()
    {
        // ∏∂¿ª ∏  æ¿ ø¨∞·«œ±‚
        SceneManager.LoadScene("Level");
    }

    public void ExitMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void setQuit()
    {
        Application.Quit();
    }
}
