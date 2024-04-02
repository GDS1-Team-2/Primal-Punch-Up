using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound; // 拾取Items的音效
    public int score = 0; // 玩家得分
    public int tempScore = 0; // 玩家的临时得分
    public int maxTempScore = 3; // 临时分数的上限，可以在Unity中修改
    public Text scoreText; // 分数显示的UI组件
    public Text tempScoreText; // 临时分数显示的UI组件
    public string testTag; // 拾取物品的标签
    public string baseTag; // 基地的标签

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(testTag) && tempScore < maxTempScore)
        {
            // 增加临时分数
            tempScore++;
            // 播放拾取音效
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            // 销毁Item对象
            Destroy(other.gameObject);
            // 更新UI显示临时分数
            UpdateTempScoreText();
        }
        else if (other.gameObject.CompareTag(baseTag))
        {
            // 将临时分数转换为正式分数
            ConvertTempScoreToScore();
        }

        // 检查分数是否达到通关条件
        // CheckForLevelCompletion();
    }

    private void Update()
    {
        // 可以在这里添加检测玩家死亡的逻辑，并调用LoseTempScore()方法
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void UpdateTempScoreText()
    {
        if (tempScoreText != null)
        {
            tempScoreText.text = tempScore.ToString();
        }
    }

    private void CheckForLevelCompletion()
    {
        if (score >= 11) // 根据需要设置通关的分数条件
        {
            //SceneManager.LoadScene("PassScene");
        }
    }

    private void ConvertTempScoreToScore()
    {
        score += tempScore;
        tempScore = 0; // 重置临时分数
        UpdateScoreText();
        UpdateTempScoreText();
    }

    public void LoseTempScore()
    {
        // 玩家死亡后丢失临时分数
        tempScore = 0;
        UpdateTempScoreText();
    }
}