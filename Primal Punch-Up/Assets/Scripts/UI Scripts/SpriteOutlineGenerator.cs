using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteOutlineGenerator : MonoBehaviour
{
    public Material outlineMaterial; // ��ʹ�����Shader�Ĳ�����ק������
    public int playerNo; // ��ұ��

    void Start()
    {
        outlineMaterial = gameObject.GetComponent<Image>().material;
        //UpdateColor();
    }

    public void UpdateColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                outlineMaterial.SetColor("_OutlineColor", Color.blue);
                break;
            case 2:
                outlineMaterial.SetColor("_OutlineColor", Color.red);
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
        UpdateColor(playerNo);
    }
}
