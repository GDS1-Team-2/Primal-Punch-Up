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
    public int maxTempBag = 3; // ��ʱ��Ʒ�����ޣ�������Unity���޸�
    public int currentTempBag = 0;
    public Text scoreText; // ������ʾ��UI���
    public Text tempScoreText; // ��ʱ������ʾ��UI���
    public string OneScoreTag; // ʰȡ��Ʒ�ı�ǩ
    public string ThreeScoreTag; // ʰȡ�����Ʒ�ı�ǩ
    public string BadScoreTag; // ʰȡ������Ʒ�ı�ǩ
    public string baseTag; // ���صı�ǩ

    public GameObject speedRangeCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(OneScoreTag) && currentTempBag < maxTempBag)
        {
            // ������ʱ����
            tempScore++;
            currentTempBag++;
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
        if (other.gameObject.CompareTag(ThreeScoreTag) && currentTempBag < maxTempBag)
        {
            // ������ʱ����
            tempScore = tempScore + 3;
            currentTempBag++;
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
        
        if (other.gameObject.CompareTag("chocolate")){
           Destroy(other.gameObject);
           this.speedRangeCollider.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("speedRange")){
           this.gameObject.GetComponent<PlayerBase>().setSpeed(true);
        }

        if (other.gameObject.CompareTag(BadScoreTag) && tempScore > 1) 
        {
            // ������ʱ����
            tempScore = tempScore - 1;
            
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
        // �������������Ӽ������������߼���������LoseTempScore()����
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
        currentTempBag = 0;
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