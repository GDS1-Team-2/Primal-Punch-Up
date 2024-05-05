using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
}
