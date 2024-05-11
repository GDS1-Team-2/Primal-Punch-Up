using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 5f;  // 磁铁作用半径
    public float magnetDuration = 10f;  // 磁铁效果持续时间
    public bool isActive = false;  // 磁铁是否激活的标志
    private float magnetTimer = 0;  // 磁铁效果的计时器
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
        if (!isActive)  // 如果磁铁目前不是激活状态，才激活
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
