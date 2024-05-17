using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 10f;  // �������ð뾶
    public float magnetDuration = 10f;  // ����Ч������ʱ��
    public bool isActive = false;  // �����Ƿ񼤻�ı�־
    private float magnetTimer = 0;  // ����Ч���ļ�ʱ��
    public Slider cooldownSlider;
    public Text timerText;
    //public Transform player;

    public GameObject magnetRangeIndicator;

    public void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            magnetTimer -= Time.deltaTime;
            cooldownSlider.value = magnetTimer;
            timerText.text = Mathf.RoundToInt(magnetTimer).ToString();

            if (magnetTimer <= 0)
            {
                DeactivateMagnet();
            }
            else
            {
                CollectNearbyItems();
            }
        }
    }

    public void ActivateMagnet()
    {
        if (!isActive)  // �������Ŀǰ���Ǽ���״̬���ż���
        {
            isActive = true;
            magnetTimer = magnetDuration;
            magnetRangeIndicator.SetActive(true); // ��ʾԲ�η�Χָʾ��
        }
    }

    private void CollectNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);
        PickupItem pickupScript = GetComponent<PickupItem>();
        foreach (var hitCollider in hitColliders)
        {
            if ((hitCollider.CompareTag(pickupScript.OneScoreTag) || hitCollider.CompareTag(pickupScript.MultiScoreTag))) { 
                StartCoroutine(MoveItemToPlayer(hitCollider.gameObject, hitCollider.CompareTag(pickupScript.OneScoreTag) ? 1 : 3, pickupScript));
            }
        }
    }

    IEnumerator MoveItemToPlayer(GameObject item, int scoreToAdd, PickupItem pickupScript)
    {
        float time = 0;
        float duration = 0.5f; // �����ƶ�����������ʱ��
        Vector3 startPosition = item.transform.position;

        // ȷ�������屻����ǰ��������λ��
        while (time < duration)
        {
            if (item == null)  // ��������Ƿ����
                yield break;  // ������岻���ڣ������˳�Э��

            item.transform.position = Vector3.Lerp(startPosition, transform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // ��Ʒ�������λ�ú��ٽ��з������Ӻ�����
        if (item != null)  // �ٴμ����ȷ��������Ȼ����
        {
            //pickupScript.AddScore(scoreToAdd);
            //pickupScript.currentTempBag++;
            if (pickupScript.pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupScript.pickupSound, transform.position);
            }
            Destroy(item);  // ���ٵ÷���Ʒ
        }
    }


    private void DeactivateMagnet()
    {
        isActive = false;
        magnetRangeIndicator.SetActive(false); // ����Բ�η�Χָʾ��
    }
}
