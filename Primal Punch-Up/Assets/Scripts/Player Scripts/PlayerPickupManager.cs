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
    public GameObject hand;
    public GameObject otherHand;

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

    public float iceDuration = 10;
    public GameObject iceRadius;
    public bool usingIce;

    //public GameObject ArrowPrefab;
    public bool hasBow = false;
    public int maxArrowNumber = 3;
    public int currentArrowNumber = 3;
    public GameObject bowHolding;

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
        iceDuration = 10;
        usingFirecracker = false;
        usingMagnet = false;
        usingIce = false;
        portalUsed = false;
        maxArrowNumber = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoMagnet()
    {
        hasItem = false;
        usingMagnet = false;
        itemText.text = "Current Item: None";
        itemIconUI.gameObject.SetActive(false);
        controlIcon.SetActive(false);
        itemCooldown.SetActive(false);
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
                if (currentItem.name == "Bow")
                {
                    itemCooldown.SetActive(true);
                    cooldownSlider.maxValue = maxArrowNumber;
                    cooldownSlider.value = maxArrowNumber;
                    currentArrowNumber = maxArrowNumber;
                    timerText.text = currentArrowNumber.ToString();
                    hasBow = true;
                    
                    if (this.gameObject.CompareTag("Lizard"))
                    {
                        //pos 0 0 0
                        //Rotation 0 190 197.9
                        bowHolding = Instantiate(currentItem, new Vector3(0, 0, 0), Quaternion.identity, hand.transform);
                        bowHolding.transform.localPosition = new Vector3(-0.000449999992f, 0.000349999988f, -0.000720000011f);
                        bowHolding.transform.localRotation = Quaternion.Euler(7.40002251f, 351.600006f, 218.700012f);
                    }
                    else if (this.gameObject.CompareTag("Bear"))
                    {
                        bowHolding = Instantiate(currentItem, new Vector3(0,0,0), Quaternion.identity, hand.transform);
                        bowHolding.transform.localPosition = new Vector3(-0.000199999995f, 0.00419999985f, 0.0037f);
                        bowHolding.transform.localRotation = Quaternion.Euler(347.300018f, 348.200012f, 266.5f);
                    }
                    else if (this.gameObject.CompareTag("Rabbit"))
                    {
                        bowHolding = Instantiate(currentItem, new Vector3(0, 0, 0), Quaternion.identity, hand.transform);
                        bowHolding.transform.localPosition = new Vector3(0.00280999998f, 0.00246999995f, 0.000829999975f);
                        bowHolding.transform.localRotation = Quaternion.Euler(285.889984f, 180, 152.430008f);
                    }
                    else if (this.gameObject.CompareTag("Fox"))
                    {
                        //pos Vector3(-0.00107,-0.00106000004,0)
                        bowHolding = Instantiate(currentItem, new Vector3(0, 0, 0), Quaternion.identity, hand.transform);
                        bowHolding.transform.localPosition = new Vector3(0, 0.000169999999f, 0.000809999998f);
                        bowHolding.transform.localRotation = Quaternion.Euler(347.790009f, -4.36766896f, 274.529999f);
                    }

                    bowHolding.GetComponent<BowAndArrowScript>().otherHand = otherHand;
                }
            }
        }
    }

    public void UseItem()
    {
        if (currentItem != null)
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
            else if ((currentItem.name == "Magnet"))
            {
                if (!usingMagnet && !MagnetItem.isActive && hasItem)
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
                if (!usingIce && hasItem)
                {
                    usingIce = true;
                    hasItem = true;
                    itemCooldown.SetActive(true);
                    cooldownSlider.maxValue = iceDuration;
                    StartCoroutine(ActivateFreeze(iceDuration));
                }
            }

            else if (currentItem.name == "Bow" && hasItem)
            {
                if (currentArrowNumber > 1)
                {
                    PlayerBase.PlayArrowAnim(bowHolding, false);
                    currentArrowNumber--;
                    timerText.text = currentArrowNumber.ToString();
                    bowHolding.GetComponent<BowAndArrowScript>().ShootArrow(transform.position, this.gameObject);
                }
                else if (currentArrowNumber == 1)
                {
                    currentArrowNumber--;
                    timerText.text = currentArrowNumber.ToString();
                    bowHolding.GetComponent<BowAndArrowScript>().ShootArrow(transform.position, this.gameObject);
                    hasBow = false;
                    currentArrowNumber = 0;
                    hasItem = false;
                    itemText.text = "Current Item: None";
                    itemIconUI.gameObject.SetActive(false);
                    controlIcon.SetActive(false);
                    itemCooldown.SetActive(false);
                    PlayerBase.PlayArrowAnim(bowHolding, true);
                    bowHolding = null;
                }
            }
        }
    }

    IEnumerator ActivateFreeze(float duration)
    {

        float elapsedTime = 0;
        GameObject iceZone = Instantiate(iceRadius, new Vector3(gameObject.transform.position.x, 999, gameObject.transform.position.z), Quaternion.identity);
        iceZone.GetComponent<IceScript>().playerNo = playerNo;

        while (elapsedTime < duration)
        {
            cooldownSlider.value = duration - elapsedTime;
            timerText.text = Mathf.RoundToInt(duration - elapsedTime).ToString();
            iceZone.transform.position = gameObject.transform.position;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 9.9)
            {
                iceZone.GetComponent<IceScript>().SetEnding(true);
                usingIce = false;
            }
            yield return null;
        }

        
        hasItem = false;
        usingIce = false;
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
