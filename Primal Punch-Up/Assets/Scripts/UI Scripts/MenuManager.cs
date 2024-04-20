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
    public string CharacterSelectSceneName;

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
        SceneManager.LoadScene(MainMenuSceneName);
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
}
