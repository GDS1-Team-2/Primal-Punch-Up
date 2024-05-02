using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutlineGenerator : MonoBehaviour
{
    public Material outlineMaterial; // ��ʹ�����Shader�Ĳ�����ק������
    public int playerNo; // ��ұ��

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

    // ��������Ϸ����ʱ�������������������ɫ
    public void SetPlayerNo(int newPlayerNo)
    {
        playerNo = newPlayerNo;
        UpdateColor();
    }
}
