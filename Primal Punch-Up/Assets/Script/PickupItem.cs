using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound; // 拾取Items的音效
    public int score = 0; // 玩家得分
    public Text scoreText; // 分数显示的UI组件
    public string testTag;//Tag

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(testTag))
        {
            // 增加分数
            score++;

            // 播放拾取音效
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // 销毁Item对象
            Destroy(other.gameObject);

            // 更新UI显示分数
            UpdateScoreText();
        }

        // 检查分数是否达到通关条件
        CheckForLevelCompletion();
    }

    private void Update()
    {
        // 检测按键R重置当前游戏场景
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentScene();
        }
        Debug.Log(score);*/
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void CheckForLevelCompletion()
    {
        if (score >= 11) // 根据需要设置通关的分数条件
        {
            SceneManager.LoadScene("PassScene");
        }
    }

    private void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // 确保时间比例被重置
    }
}
