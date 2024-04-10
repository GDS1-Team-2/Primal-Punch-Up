using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform player; // ��ҵ�Transform
    public float heightAbovePlayer = 100f; // ͼ�������ͷ���Ϸ��ĸ߶�

    void Update()
    {
        // ����ͼ���λ�ã�ʹ���������ҵ�x��z���꣬y�����������Ҫ�����߶�
        transform.position = new Vector3(player.position.x, player.position.y + heightAbovePlayer, player.position.z);
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
