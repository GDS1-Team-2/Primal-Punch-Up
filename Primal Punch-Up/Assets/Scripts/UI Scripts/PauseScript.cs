using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject tutorialBox;
    public List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        if (PlayerPrefs.GetInt("RoundNo") == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            tutorialBox.SetActive(false);
        }
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitTutorial()
    {
        Time.timeScale = 1;
        if (tutorialBox != null)
        {
            tutorialBox.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerBase>().acceptInput = false;
        }
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerBase>().acceptInput = true;
        }
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

    }
}
