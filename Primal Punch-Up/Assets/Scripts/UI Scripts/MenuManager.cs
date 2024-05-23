using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string OptionsSceneName;
    public string Map1SceneName;
    public string Map2SceneName;
    public string Map3SceneName;
    public string MainMenuSceneName;
    public string MapSelectSceneName;
    public string CharacterSelectSceneName;
    public string PlayerNumberSelectSceneName;

    public bool reset = false;
    public PlayerNoSelect PlayerNoSelect;

    public void OpenOptions()
    {
        Debug.Log("Options clicked. Implement according to the project's UI setup.");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game requested.");
        Application.Quit(); 
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }

    public void LoadMap1()
    {
        PlayerPrefs.SetInt("Map", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(Map1SceneName);
    }

    public void LoadMap2()
    {
        PlayerPrefs.SetInt("Map", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(Map2SceneName);
    }

    public void LoadMap3()
    {
        PlayerPrefs.SetInt("Map", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene(Map3SceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene(CharacterSelectSceneName);
    }

    public void LoadMapSelect()
    {
        SceneManager.LoadScene(MapSelectSceneName);
    }

    public void ChangeMap()
    {
        if (PlayerPrefs.GetInt("Map") == 1)
        {
            LoadMap2();
        }
        else if (PlayerPrefs.GetInt("Map") == 2)
        {
            LoadMap3();
        }
        else if (PlayerPrefs.GetInt("Map") == 3)
        {
            LoadMap1();
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == MainMenuSceneName ||
            SceneManager.GetActiveScene().name == CharacterSelectSceneName ||
            SceneManager.GetActiveScene().name == MapSelectSceneName ||
            SceneManager.GetActiveScene().name == PlayerNumberSelectSceneName)
        {
            PlayerPrefs.SetInt("Player1Wins", 0);
            PlayerPrefs.SetInt("Player2Wins", 0);
            PlayerPrefs.SetInt("Player3Wins", 0);
            PlayerPrefs.SetInt("Player4Wins", 0);
            PlayerPrefs.SetInt("RoundNo", 1);
            PlayerPrefs.SetInt("ScoreKey1", 0);
            PlayerPrefs.SetInt("ScoreKey2", 0);
            PlayerPrefs.SetInt("ScoreKey3", 0);
            PlayerPrefs.SetInt("ScoreKey4", 0);
            PlayerPrefs.Save();
        }
    }

    public void Set2Players()
    {
        PlayerPrefs.SetInt("noOfPlayers", 2);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfPlayers"));
        PlayerNoSelect.ActivateButton();
    }

    public void Set3Players()
    {
        PlayerPrefs.SetInt("noOfPlayers", 3);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfPlayers"));
        PlayerNoSelect.ActivateButton();
    }

    public void Set4Players()
    {
        PlayerPrefs.SetInt("noOfPlayers", 4);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfPlayers"));
        PlayerNoSelect.ActivateButton();
    }

    public void Set3Rounds()
    {
        PlayerPrefs.SetInt("noOfRounds", 3);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfRounds"));
        PlayerNoSelect.ActivateButton();
    }

    public void Set5Rounds()
    {
        PlayerPrefs.SetInt("noOfRounds", 5);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfRounds"));
        PlayerNoSelect.ActivateButton();
    }

    public void Set7Rounds()
    {
        PlayerPrefs.SetInt("noOfRounds", 7);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("noOfRounds"));
        PlayerNoSelect.ActivateButton();
    }

    

    public void LoadPlayerNumberSelect()
    {
        PlayerPrefs.SetInt("noOfRounds", 0);
        PlayerPrefs.SetInt("noOfPlayers", 0);
        SceneManager.LoadScene(PlayerNumberSelectSceneName);
    }

    public void TwoPlayerLoad()
    {
        PlayerPrefs.SetInt("noOfPlayers", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(CharacterSelectSceneName);
    }

    public void ThreePlayerLoad()
    {
        PlayerPrefs.SetInt("noOfPlayers", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene(CharacterSelectSceneName);
    }

    public void FourPlayerLoad()
    {
        PlayerPrefs.SetInt("noOfPlayers", 4);
        PlayerPrefs.Save();
        SceneManager.LoadScene(CharacterSelectSceneName);
    }
}
