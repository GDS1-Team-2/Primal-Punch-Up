using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject currentItem;
    public bool hasItem = false;
    public bool usingMagnet = false;

    public PlayerBase PlayerBase;
    public int playerNo;

    public Text itemText;

    public MagnetItem MagnetItem;

    //add icon
    public Image itemIconUI; // UI Image component to display the item icon
    // Start is called before the first frame update
    void Start()
    {
        MagnetItem = gameObject.GetComponent<MagnetItem>();
        PlayerBase = gameObject.GetComponent<PlayerBase>();
        playerNo = PlayerBase.playerNo;
        string playerItemUi = "Player" + playerNo + "CurrentItemText";
        itemText = GameObject.Find(playerItemUi).GetComponent<Text>();
        string playerIconUi = "Player" + playerNo + "CurrentItemIcon";
        itemIconUI = GameObject.Find(playerIconUi).GetComponent<Image>();
        itemIconUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMagnet && !MagnetItem.isActive)
        {
            hasItem = false;
            usingMagnet = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //pickup items:
        if (!hasItem)
        {
            if (other.gameObject.CompareTag("Pickup"))
            {
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);
                hasItem = true;
                int rand = Random.Range(0, items.Count);
                currentItem = items[rand];
                string uitext = "Current Item: " + currentItem.name;
                itemText.text = uitext;

                // Update the UI icon for the current item
                if (currentItem.GetComponent<Ui_icon>() != null)
                {
                    itemIconUI.gameObject.SetActive(true);
                    itemIconUI.sprite = currentItem.GetComponent<Ui_icon>().itemIcon;
                     // Ensure the icon is visible
                }
            }
        }
    }

    public void UseItem()
    {
        if (currentItem.name == "Trap")
        {
            currentItem.GetComponent<TrapScript>().playerNo = playerNo;
            currentItem = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity);
            hasItem = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
        }
        else if (currentItem.name == "Landmine")
        {
            currentItem.GetComponent<LandmineScript>().playerNo = playerNo;
            currentItem = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity);
            hasItem = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
        }
        else if((currentItem.name == "Magnet")) {
            MagnetItem.ActivateMagnet();
            usingMagnet = true;
            hasItem = true;
        }
            
    }

}
