using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }

    public void Return()
    {
        menu.SetActive(false);
    }

    public void Hero()
    {
        // ∏∂¿ª ∏  æ¿ ø¨∞·«œ±‚
        SceneManager.LoadScene("Charactor");
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
