using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectScript : MonoBehaviour
{
    public string Map1SceneName;
    public string Map2SceneName;
    public string MainMenuSceneName;
    // Start is called before the first frame update

    public void LoadMap1()
    {
        SceneManager.LoadScene(Map1SceneName); 
    }

    public void LoadMap2()
    {
        SceneManager.LoadScene(Map2SceneName);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
