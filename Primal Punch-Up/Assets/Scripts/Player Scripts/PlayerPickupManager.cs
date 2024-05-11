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
    public GameObject controlIcon;

    public MagnetItem MagnetItem;

    public GameObject teleportGatePrefab;
    private GameObject firstPortal = null;
    private GameObject secondPortal = null;
    private bool hasTeleportGate = false;
    public GameObject flamePrefab;
    public bool hasFlameItem = false;
    public float flameTrailDuration = 5.0f;
    public float flameSpawnInterval = 0.5f;
    public float flameLifetime = 1.0f;

    private bool firstPlaced = false;
    private bool secondPlaced = false;

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
        switch (playerNo)
        {
            case 1:
                controlIcon = GameObject.Find("Control N");
                break;
            case 2:
                controlIcon = GameObject.Find("Control ]");
                break;
            case 3:
                controlIcon = GameObject.Find("Control Square");
                break;
            case 4:
                controlIcon = GameObject.Find("Control Square");
                break;
        }
        controlIcon.SetActive(false);
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
            controlIcon.SetActive(false);
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
                controlIcon.SetActive(true);

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
            controlIcon.SetActive(false);
        }
        else if (currentItem.name == "Landmine")
        {
            currentItem.GetComponent<LandmineScript>().playerNo = playerNo;
            currentItem = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity);
            hasItem = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
            controlIcon.SetActive(false);
        }
        else if((currentItem.name == "Magnet")) {
            MagnetItem.ActivateMagnet();
            usingMagnet = true;
            hasItem = true;
        }
        else if (currentItem.name == "Firecracker")
        {
            StartCoroutine(CreateFlameTrail());
        }
        else if (currentItem.name == "Portal")
        {
            if (!firstPlaced)
            {
                PlacePortal();
            }
            else if (firstPlaced && !secondPlaced)
            {
                PlacePortal();
            }
        }
    }

    void PlacePortal()
    {
        if (!firstPlaced)
        {
            firstPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            firstPortal.name = "FirstPortal";
        }
        else if (firstPlaced && !secondPlaced)
        {
            secondPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            secondPortal.name = "SecondPortal";
            firstPortal.GetComponent<Portal>().SetPartner(secondPortal);
            secondPortal.GetComponent<Portal>().SetPartner(firstPortal);
            hasItem = false;
            firstPlaced = false;
            secondPlaced = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
        }
    }

    IEnumerator CreateFlameTrail()
    {
        float endTime = Time.time + flameTrailDuration;
        while (Time.time <= endTime)
        {
            GameObject flame = Instantiate(flamePrefab, transform.position, Quaternion.identity);
            flame.GetComponent<Flame>().playerNo = PlayerBase.playerNo;
            Destroy(flame, flameLifetime);
            yield return new WaitForSeconds(flameSpawnInterval);
        }
        hasItem = false;
        currentItem = null;
        itemText.text = "Current Item: None";
        itemIconUI.gameObject.SetActive(false);
        controlIcon.SetActive(false);
    }

}
