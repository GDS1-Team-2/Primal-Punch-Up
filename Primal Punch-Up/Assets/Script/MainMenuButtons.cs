using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public string demoSceneName;
    public string startGameSceneName;
    public string optionsSceneName;
    public void PlayDemo()
    {
        SceneManager.LoadScene(demoSceneName); // 加载Demo场景，替换为实际场景名
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startGameSceneName); // 加载主游戏场景，替换为实际场景名
    }

    public void OpenOptions()
    {
        // 选项逻辑，如果是在当前场景加载UI，则不需要加载新场景
        // 可以启用一个隐藏的选项菜单Panel或者加载一个新的场景
        Debug.Log("Options clicked. Implement according to the project's UI setup.");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game requested.");
        Application.Quit(); // 退出游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 如果在Unity编辑器中，这行代码将停止播放模式
#endif
    }
}