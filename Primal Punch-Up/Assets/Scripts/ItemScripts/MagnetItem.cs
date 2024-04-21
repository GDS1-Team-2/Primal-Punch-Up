using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 5f;  // 磁铁作用半径
    public float magnetDuration = 10f;  // 磁铁效果持续时间
    private bool isActive = false;  // 磁铁是否激活的标志
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagnetItem"))
        {
            ActivateMagnet();
            Destroy(other.gameObject);  // 销毁磁铁道具
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
            if (hitCollider.CompareTag(pickupScript.OneScoreTag) && pickupScript.currentTempBag < pickupScript.maxTempBag)
            {
                pickupScript.AddScore(1);
                pickupScript.currentTempBag++;
                Destroy(hitCollider.gameObject);  // 销毁得分物品
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
                Destroy(hitCollider.gameObject);  // 销毁得分物品
            }
        }
    }

    private void DeactivateMagnet()
    {
        isActive = false;
    }
}
