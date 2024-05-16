using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float magnetRadius = 10f;  // 磁铁作用半径
    public float magnetDuration = 10f;  // 磁铁效果持续时间
    public bool isActive = false;  // 磁铁是否激活的标志
    private float magnetTimer = 0;  // 磁铁效果的计时器
    //public Transform player;

    public GameObject magnetRangeIndicator;
    void Update()
    {
        if (isActive)
        {
            magnetTimer -= Time.deltaTime;
            magnetRangeIndicator.transform.position = transform.position + new Vector3(0, 0.1f, 0); // 持续更新位置以跟随玩家

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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagnetItem"))
        {
            ActivateMagnet();
            Destroy(other.gameObject);  // 销毁磁铁道具
        }
    }*/

    public void ActivateMagnet()
    {
        if (!isActive)  // 如果磁铁目前不是激活状态，才激活
        {
            isActive = true;
            magnetTimer = magnetDuration;
            magnetRangeIndicator.transform.position = transform.position + new Vector3(0, 0.1f, 0); //
            magnetRangeIndicator.SetActive(true); // 显示圆形范围指示器
        }
    }

    private void CollectNearbyItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);
        PickupItem pickupScript = GetComponent<PickupItem>();
        foreach (var hitCollider in hitColliders)
        {
            if ((hitCollider.CompareTag(pickupScript.OneScoreTag) || hitCollider.CompareTag(pickupScript.ThreeScoreTag)) && pickupScript.currentTempBag < pickupScript.maxTempBag)
            {
                StartCoroutine(MoveItemToPlayer(hitCollider.gameObject, hitCollider.CompareTag(pickupScript.OneScoreTag) ? 1 : 3, pickupScript));
            }
        }
    }

    IEnumerator MoveItemToPlayer(GameObject item, int scoreToAdd, PickupItem pickupScript)
    {
        float time = 0;
        float duration = 0.5f; // 定义移动到玩家所需的时间
        Vector3 startPosition = item.transform.position;

        // 确保在物体被销毁前持续更新位置
        while (time < duration)
        {
            if (item == null)  // 检查物体是否存在
                yield break;  // 如果物体不存在，立即退出协程

            item.transform.position = Vector3.Lerp(startPosition, transform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // 物品到达玩家位置后再进行分数添加和销毁
        if (item != null)  // 再次检查以确保物体仍然存在
        {
            //pickupScript.AddScore(scoreToAdd);
            //pickupScript.currentTempBag++;
            if (pickupScript.pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupScript.pickupSound, transform.position);
            }
            Destroy(item);  // 销毁得分物品
        }
    }


    private void DeactivateMagnet()
    {
        isActive = false;
        magnetRangeIndicator.SetActive(false); // 隐藏圆形范围指示器
    }
}
