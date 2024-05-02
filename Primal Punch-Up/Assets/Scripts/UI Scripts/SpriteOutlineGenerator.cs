using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutlineGenerator : MonoBehaviour
{
    public Material outlineMaterial; // 将使用描边Shader的材质拖拽到这里
    public int playerNo; // 玩家编号

    void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        switch (playerNo)
        {
            case 1:
                outlineMaterial.SetColor("_OutlineColor", Color.red);
                break;
            case 2:
                outlineMaterial.SetColor("_OutlineColor", Color.blue);
                break;
            case 3:
                outlineMaterial.SetColor("_OutlineColor", Color.green);
                break;
            case 4:
                outlineMaterial.SetColor("_OutlineColor", Color.yellow);
                break;
            default:
                outlineMaterial.SetColor("_OutlineColor", Color.black);
                break;
        }
    }

    // 可以在游戏中随时调用这个方法来更新颜色
    public void SetPlayerNo(int newPlayerNo)
    {
        playerNo = newPlayerNo;
        UpdateColor();
    }
}
