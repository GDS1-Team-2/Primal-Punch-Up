using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManger : MonoBehaviour
{
    private static BgmManger instance;
    private AudioSource audioSource;

    void Awake()
    {
        // ���ʵ���Ѿ������Ҳ��ǵ�ǰʵ���������ٵ�ǰʵ����ȷ��ֻ��һ�����ֹ�����ʵ��
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // �������õ�ǰʵ��ΪΨһʵ������ȷ�����ڳ����л�ʱ���ᱻ����
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // ��ȡ�����AudioSource���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��ʼ���ű�������
        audioSource.loop = true; // ����Ϊѭ������
        audioSource.Play();

        // ���ĳ����л��¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // ȡ�����ĳ����л��¼�����ֹ�ڴ�й©
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ��鵱ǰ�����Ƿ�Ϊ��Ϸ������������Ϸ����������Ϊ "GameScene"
        if (scene.name == "Map 1" || scene.name == "Map 2" || scene.name == "Map 3" || scene.name == "Complete")
        {
            // �������ֹ�����ʵ��
            Destroy(this.gameObject);
            instance = null; // ���ʵ���������ڷ��ز˵�ʱ���´���
        }
    }
}
