using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public ParticleSystem particles;
    public SphereCollider sphereCol;
    private float randTime;
    private float timeUntilStart = 0f;
    private bool areaActive = false;
    public float areaCountDown = 20.0f;
    private float increaseScoreTimer = 0.0f;
    public float increaseInterval = 2.0f;
    private List<PickupItem> pickUpItems = new List<PickupItem>();
    public GameObject areaSpawnWarning;
    public GameObject giantPear;
    public GameObject spawnSFX;
    public AudioSource increaseScoreSFX;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        sphereCol = GetComponent<SphereCollider>();
        randTime = Random.Range(30f, 90f - areaCountDown);
        areaSpawnWarning = GameObject.Find("HarvestHaloCanvas");
        areaSpawnWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilStart += 1f * Time.deltaTime;

        if (areaActive)
        {
            areaCountDown -= 1f * Time.deltaTime;
            if (areaCountDown <= 0)
            {
                sphereCol.enabled = false;
                giantPear.SetActive(false);
                areaCountDown = 0;
            }
        }

        if (timeUntilStart >= randTime - 5f && timeUntilStart <= randTime)
        {
            areaSpawnWarning.SetActive(true);
        }

        if (timeUntilStart >= randTime && areaActive == false)
        {
            areaSpawnWarning.SetActive(false);
            particles.Play();
            StartCoroutine(GiantPearSpawn());
            spawnSFX.SetActive(true);
            sphereCol.enabled = true;
            areaActive = true;
            areaCountDown = 20.0f;
        }

        increaseScoreTimer += 1 * Time.deltaTime;
        if (increaseScoreTimer > increaseInterval)
        {
            increaseScoreTimer = increaseInterval + 1;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger)
        {
            PickupItem pickUpItemScript = other.GetComponent<PickupItem>();

            if (!pickUpItems.Contains(pickUpItemScript))
            {
                pickUpItems.Add(pickUpItemScript);
            }

            if (increaseScoreTimer >= increaseInterval)
            {
                foreach (PickupItem player in pickUpItems)
                {
                    PlayerBase playerScript = player.GetComponent<PlayerBase>();
                    if (playerScript.isDead)
                    {
                        pickUpItems.Remove(player);
                    }
                    else
                    {
                        increaseScoreSFX.Play();
                        player.score += 1;
                        player.UpdateScoreText();
                        PickupUIScript pickupUIScript = player.GetComponent<PickupUIScript>();
                        pickupUIScript.PopUp("plus", 1);
                    }
                }
                increaseScoreTimer = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupItem pickUpItemScript = other.GetComponent<PickupItem>();
        pickUpItems.Remove(pickUpItemScript);
    }

    IEnumerator GiantPearSpawn()
    {
        yield return new WaitForSeconds(1.0f);
        giantPear.SetActive(true);
    }
}
