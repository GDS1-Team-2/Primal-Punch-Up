using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 5f;  // �������ð뾶
    public float magnetDuration = 10f;  // ����Ч������ʱ��
    private bool isActive = false;  // �����Ƿ񼤻�ı�־
    private float magnetTimer = 0;  // ����Ч���ļ�ʱ��
    void Update()
    {
        if (isActive)
        {
            magnetTimer -= Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagnetItem"))
        {
            ActivateMagnet();
            Destroy(other.gameObject);  // ���ٴ�������
        }
    }

    public void ActivateMagnet()
    {
        if (!isActive)  // �������Ŀǰ���Ǽ���״̬���ż���
        {
            isActive = true;
            magnetTimer = magnetDuration;
        }
    }

    private void CollectNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);
        PickupItem pickupScript = GetComponent<PickupItem>();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(pickupScript.OneScoreTag) && pickupScript.currentTempBag < pickupScript.maxTempBag)
            {
                pickupScript.AddScore(1);
                pickupScript.currentTempBag++;
                Destroy(hitCollider.gameObject);  // ���ٵ÷���Ʒ
                if (pickupScript.pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupScript.pickupSound, transform.position);
                }
            }
            else if (hitCollider.CompareTag(pickupScript.ThreeScoreTag) && pickupScript.currentTempBag < pickupScript.maxTempBag)
            {
                pickupScript.AddScore(3);
                pickupScript.currentTempBag++;
                if (pickupScript.pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupScript.pickupSound, transform.position);
                }
                Destroy(hitCollider.gameObject);  // ���ٵ÷���Ʒ
            }
        }
    }

    private void DeactivateMagnet()
    {
        isActive = false;
    }
}
