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
            LoadMap1();
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
            PlayerPrefs.SetInt("noOfPlayers", 0);
            PlayerPrefs.SetString("Player1Model", "");
            PlayerPrefs.SetString("Player2Model", "");
            PlayerPrefs.SetString("Player3Model", "");
            PlayerPrefs.SetString("Player4Model", "");
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
    }

    public void Set3Players()
    {
        PlayerPrefs.SetInt("noOfPlayers", 3);
        PlayerPrefs.Save();
    }

    public void Set4Players()
    {
        PlayerPrefs.SetInt("noOfPlayers", 4);
        PlayerPrefs.Save();
    }

    public void LoadPlayerNumberSelect()
    {
        SceneManager.LoadScene(PlayerNumberSelectSceneName);
    }
}
