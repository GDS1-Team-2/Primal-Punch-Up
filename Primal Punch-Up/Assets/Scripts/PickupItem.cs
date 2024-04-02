using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound; // ʰȡItems����Ч
    public int score = 0; // ��ҵ÷�
    public int tempScore = 0; // ��ҵ���ʱ�÷�
    public int maxTempScore = 3; // ��ʱ���������ޣ�������Unity���޸�
    public Text scoreText; // ������ʾ��UI���
    public Text tempScoreText; // ��ʱ������ʾ��UI���
    public string testTag; // ʰȡ��Ʒ�ı�ǩ
    public string baseTag; // ���صı�ǩ

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(testTag) && tempScore < maxTempScore)
        {
            // ������ʱ����
            tempScore++;
            // ����ʰȡ��Ч
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            // ����Item����
            Destroy(other.gameObject);
            // ����UI��ʾ��ʱ����
            UpdateTempScoreText();
        }
        else if (other.gameObject.CompareTag(baseTag))
        {
            // ����ʱ����ת��Ϊ��ʽ����
            ConvertTempScoreToScore();
        }

        // �������Ƿ�ﵽͨ������
        // CheckForLevelCompletion();
    }

    private void Update()
    {
        // ������������Ӽ������������߼���������LoseTempScore()����
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
        if (score >= 11) // ������Ҫ����ͨ�صķ�������
        {
            //SceneManager.LoadScene("PassScene");
        }
    }

    private void ConvertTempScoreToScore()
    {
        score += tempScore;
        tempScore = 0; // ������ʱ����
        UpdateScoreText();
        UpdateTempScoreText();
    }

    public void LoseTempScore()
    {
        // ���������ʧ��ʱ����
        tempScore = 0;
        UpdateTempScoreText();
    }
}