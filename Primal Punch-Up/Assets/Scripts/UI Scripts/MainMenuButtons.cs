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
        SceneManager.LoadScene(demoSceneName); // ����Demo�������滻Ϊʵ�ʳ�����
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startGameSceneName); // ��������Ϸ�������滻Ϊʵ�ʳ�����
    }

    public void OpenOptions()
    {
        // ѡ���߼���������ڵ�ǰ��������UI������Ҫ�����³���
        // ��������һ�����ص�ѡ��˵�Panel���߼���һ���µĳ���
        Debug.Log("Options clicked. Implement according to the project's UI setup.");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game requested.");
        Application.Quit(); // �˳���Ϸ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Unity�༭���У����д��뽫ֹͣ����ģʽ
#endif
    }
}