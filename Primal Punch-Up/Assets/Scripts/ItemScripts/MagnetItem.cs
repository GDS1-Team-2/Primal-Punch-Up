using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 5f;  // �������ð뾶
    public float magnetDuration = 10f;  // ����Ч������ʱ��
    public bool isActive = false;  // �����Ƿ񼤻�ı�־
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
            if (hitCollider.CompareTag(pickupScript.OneScoreTag))
            {
                pickupScript.AddScore(hitCollider.gameObject, 1);
            }
            else if (hitCollider.CompareTag(pickupScript.MultiScoreTag))
            {
                pickupScript.AddScore(hitCollider.gameObject, 3);
            }
        }
    }

    private void DeactivateMagnet()
    {
        isActive = false;
    }
}
