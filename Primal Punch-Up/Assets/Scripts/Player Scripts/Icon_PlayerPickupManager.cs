using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon_PlayerPickupManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>(); // �����б�
    public GameObject currentItem; // ��ǰ���еĵ���
    public bool hasItem = false; // �Ƿ���е���

    public PlayerBase PlayerBase;
    public int playerNo;

    public GameObject[] itemIcons; // ������ʾ����ͼ���UI Image�������

    public MagnetItem MagnetItem;

    void Start()
    {
        MagnetItem = GetComponent<MagnetItem>();
        PlayerBase = GetComponent<PlayerBase>();
        playerNo = PlayerBase.playerNo;
        switch (playerNo)
        {
            case 1:
                itemIcons = GameObject.FindGameObjectsWithTag("Player1CurrentItemIcon");
                break;
            /*case 2:
                itemIcons = GameObject.FindGameObjectsWithTag("Player2CurrentItemIcon");
                break;
            case 3:
                itemIcons = GameObject.FindGameObjectsWithTag("Player3CurrentItemIcon");
                break;
            case 4:
                itemIcons = GameObject.FindGameObjectsWithTag("Player4CurrentItemIcon");
                break;*/
        }

        // ��ʼ��ʱ��������ͼ��
        foreach (GameObject icon in itemIcons)
        {
            icon.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pickup items:
        if (!hasItem && other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            hasItem = true;
            int rand = Random.Range(0, items.Count);
            currentItem = items[rand];

            // ����UIͼ��
            foreach (GameObject icon in itemIcons)
            {
                icon.GetComponent<Image>().sprite = currentItem.GetComponent<Ui_icon>().itemIcon;
                icon.SetActive(true);
            }
        }
    }

    public void UseItem()
    {
        if (hasItem)
        {
            if (currentItem.name == "Trap")
            {
                currentItem.GetComponent<TrapScript>().playerNo = playerNo;
                currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);
            }
            else if (currentItem.name == "Landmine")
            {
                currentItem.GetComponent<LandmineScript>().playerNo = playerNo;
                currentItem = Instantiate(currentItem, transform.position, Quaternion.identity);
            }
            else if (currentItem.name == "MagnetItem")
            {
                MagnetItem.ActivateMagnet();
            }

            hasItem = false;
            // ����ʹ�ú�����ͼ��
            foreach (GameObject icon in itemIcons)
            {
                icon.SetActive(false);
            }
        }
    }
}
