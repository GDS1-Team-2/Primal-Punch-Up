using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject currentItem;

    public AudioSource AudioSource;

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
    public float flameSpawnInterval = 0.1f;
    public float flameLifetime = 1.0f;
    public bool usingFirecracker = false;

    public float iceDuration;
    public GameObject iceRadius;

    private bool firstPlaced = false;
    private bool secondPlaced = false;
    private bool canPlace = false;
    private bool portalUsed = false;

    //add icon
    public Image itemIconUI;
    public GameObject itemCooldown;
    private Slider cooldownSlider;
    public GameObject itemTimer;
    private Text timerText;
    // UI Image component to display the item icon
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
        string playerCooldownUI = "Player" + playerNo + "ItemCooldown";
        itemCooldown = GameObject.Find(playerCooldownUI);
        cooldownSlider = itemCooldown.GetComponent<Slider>();
        string playerCooldownTimerUI = "Player" + playerNo + "ItemTimer";
        itemTimer = GameObject.Find(playerCooldownTimerUI);
        timerText = itemTimer.GetComponent<Text>();
        itemCooldown.SetActive(false);
        string c = "P" + playerNo + "ItemControl";
        controlIcon = GameObject.FindGameObjectWithTag(c);
        controlIcon.SetActive(false);

        usingFirecracker = false;
        usingMagnet = false;
        portalUsed = false;
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
            itemCooldown.SetActive(false);
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
            if (!usingMagnet)
            {
                MagnetItem.ActivateMagnet();
                usingMagnet = true;
                hasItem = true;
                itemCooldown.SetActive(true);
                cooldownSlider.maxValue = MagnetItem.magnetDuration;
                MagnetItem.cooldownSlider = cooldownSlider;
                MagnetItem.timerText = timerText;
            }
            
        }
        else if (currentItem.name == "Firecracker")
        {
            if (!usingFirecracker)
            {
                StartCoroutine(CreateFlameTrail());
            }
        }
        else if (currentItem.name == "Portal")
        {
            if (!portalUsed)
            {
                if (!firstPlaced)
                {
                    PlacePortal();
                }
                else if (firstPlaced && !secondPlaced)
                {
                    PlacePortal();
                    portalUsed = true;
                }
            }
        }
        else if (currentItem.name == "Freeze")
        {
            hasItem = true;
            itemCooldown.SetActive(true);
            cooldownSlider.maxValue = iceDuration;
            StartCoroutine(ActivateIceGlove(10));
            
        }
    }

    IEnumerator ActivateIceGlove(float duration)
    {
        float elapsedTime = 0;
        GameObject iceZone = Instantiate(iceRadius, new Vector3(gameObject.transform.position.x, 999, gameObject.transform.position.z), Quaternion.identity);
        iceZone.GetComponent<IceScript>().playerNo = playerNo;

        while (elapsedTime < duration)
        {
            cooldownSlider.value = elapsedTime - duration;
            timerText.text = Mathf.RoundToInt(duration - elapsedTime).ToString();
            iceZone.transform.position = gameObject.transform.position;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 9.9)
            {
                iceZone.GetComponent<IceScript>().SetEnding(true);
            }
            yield return null;
        }

        
        hasItem = false;
        itemCooldown.SetActive(false);

        Destroy( iceZone );
        itemText.text = "Current Item: None";
        itemIconUI.gameObject.SetActive(false);
        controlIcon.SetActive(false);

    }


    void PlacePortal()
    {
        if (!firstPlaced)
        {
            firstPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            firstPortal.GetComponent<Portal>().SetColour(playerNo);
            firstPortal.name = "FirstPortal";
            StartCoroutine(PortalTime());
        }
        else if (firstPlaced && !secondPlaced && canPlace)
        {
            secondPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            secondPortal.GetComponent<Portal>().SetColour(playerNo);
            secondPortal.name = "SecondPortal";
            firstPortal.GetComponent<Portal>().SetPartner(secondPortal);
            secondPortal.GetComponent<Portal>().SetPartner(firstPortal);
            hasItem = false;
            secondPlaced = true;
            canPlace = false;
            itemText.text = "Current Item: None";
            itemIconUI.gameObject.SetActive(false);
            items.RemoveAt(items.Count-1);
            controlIcon.SetActive(false);
        }
    }

    IEnumerator PortalTime()
    {
        firstPlaced = true;
        yield return new WaitForSeconds(1.0f);
        canPlace = true;
    }

    IEnumerator CreateFlameTrail()
    {
        usingFirecracker = true;
        AudioSource.Play();
        StartCoroutine(FlameTimer());
        float elapsedTime = 0;
        float duration = flameTrailDuration;
        while (elapsedTime < duration)
        {
            GameObject flame = Instantiate(flamePrefab, transform.position, Quaternion.identity);
            flame.GetComponent<Flame>().playerNo = PlayerBase.playerNo;
            Destroy(flame, flameLifetime);
            //yield return new WaitForSeconds(flameSpawnInterval);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hasItem = false;
        currentItem = null;
        itemText.text = "Current Item: None";
        itemIconUI.gameObject.SetActive(false);
        controlIcon.SetActive(false);
        itemCooldown.SetActive(false);
        AudioSource.Stop();
        usingFirecracker = false;
    }

    IEnumerator FlameTimer()
    {
        itemCooldown.SetActive(true);
        float flameTimer = flameTrailDuration;
        cooldownSlider.maxValue = flameTrailDuration;
        float elapsedTime = 0;
        float duration = flameTrailDuration;
        while (elapsedTime < duration)
        {
            flameTimer -= Time.deltaTime;
            cooldownSlider.value = flameTimer;
            timerText.text = Mathf.RoundToInt(flameTimer).ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
