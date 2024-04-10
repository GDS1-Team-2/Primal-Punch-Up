using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    public float heightAbovePlayer = 100f; // 图标在玩家头顶上方的高度

    void Update()
    {
        // 更新图标的位置，使其仅跟随玩家的x和z坐标，y坐标则根据需要调整高度
        transform.position = new Vector3(player.position.x, player.position.y + heightAbovePlayer, player.position.z);
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
