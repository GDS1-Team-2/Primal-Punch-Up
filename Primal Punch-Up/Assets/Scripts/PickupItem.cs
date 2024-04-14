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

    public GameObject blueEffectPrefab;

    float speedRangeDeactivationTime = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(OneScoreTag) && currentTempBag < maxTempBag)
        {

            tempScore++;
            currentTempBag++;
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            
            Destroy(other.gameObject);
            UpdateTempScoreText();
        }
        if (other.gameObject.CompareTag(ThreeScoreTag) && currentTempBag < maxTempBag)
        {
            
            tempScore = tempScore + 3;
            currentTempBag++;
            
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            
            Destroy(other.gameObject);
            
            UpdateTempScoreText();
        }
        if (other.gameObject.CompareTag("chocolate")){
           Destroy(other.gameObject);
           this.speedRangeCollider.gameObject.SetActive(true);
           GameObject newPrefab = Instantiate(blueEffectPrefab, this.transform.position, this.transform.rotation);
           newPrefab.transform.parent = this.transform;
           newPrefab.transform.localScale *= 10;
           StartCoroutine(DeactivateNodeAfterTime(newPrefab));
        }
        if (other.gameObject.CompareTag("speedRange")){
           this.gameObject.GetComponent<PlayerBase>().setSpeed(true);
           StartCoroutine(recoverSpeed());
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

    IEnumerator DeactivateNodeAfterTime(GameObject effect)
    {
        // 等待指定的时间
        yield return new WaitForSeconds(speedRangeDeactivationTime);

        // 将节点的活动状态设置为 false
         this.speedRangeCollider.gameObject.SetActive(false);
        Destroy(effect);
    }

    IEnumerator recoverSpeed()
    {
        // 等待指定的时间
        yield return new WaitForSeconds(speedRangeDeactivationTime);

        // 将节点的活动状态设置为 false
        this.gameObject.GetComponent<PlayerBase>().setSpeed(false);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("speedRange")){
           this.gameObject.GetComponent<PlayerBase>().setSpeed(false);
        }
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

    public void UpdateTempScoreText()
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

    public void AddScore(int scoreToAdd)
    {
            tempScore += scoreToAdd;
            UpdateTempScoreText();
    }
}