using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound; // ʰȡItems����Ч
    public int score = 0; // ��ҵ÷�
    public Text scoreText; // ������ʾ��UI���
    public string testTag;//Tag

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(testTag))
        {
            // ���ӷ���
            score++;

            // ����ʰȡ��Ч
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // ����Item����
            Destroy(other.gameObject);

            // ����UI��ʾ����
            UpdateScoreText();
        }

        // �������Ƿ�ﵽͨ������
        CheckForLevelCompletion();
    }

    private void Update()
    {
        // ��ⰴ��R���õ�ǰ��Ϸ����
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
        if (score >= 11) // ������Ҫ����ͨ�صķ�������
        {
            SceneManager.LoadScene("PassScene");
        }
    }

    private void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // ȷ��ʱ�����������
    }
}
