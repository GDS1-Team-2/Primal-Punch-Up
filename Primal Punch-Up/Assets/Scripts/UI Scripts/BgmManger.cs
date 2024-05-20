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
        // 如果实例已经存在且不是当前实例，则销毁当前实例，确保只有一个音乐管理器实例
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // 否则设置当前实例为唯一实例，并确保它在场景切换时不会被销毁
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 开始播放背景音乐
        audioSource.loop = true; // 设置为循环播放
        audioSource.Play();

        // 订阅场景切换事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 取消订阅场景切换事件，防止内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查当前场景是否为游戏场景，假设游戏场景的名称为 "GameScene"
        if (scene.name == "Map 1" || scene.name == "Map 2" || scene.name == "Map 3" || scene.name == "Complete")
        {
            // 销毁音乐管理器实例
            Destroy(this.gameObject);
            instance = null; // 清空实例，允许在返回菜单时重新创建
        }
    }
}
