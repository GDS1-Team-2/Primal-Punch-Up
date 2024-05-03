using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    //follow is for pickup score item
    public AudioClip pickupSound; // Sound effect for picking up items
    public int score = 0; // Player's score
    public int tempScore = 0; // Player's temporary score
    public int maxTempBag = 3; // Maximum capacity of temporary items, can be modified in Unity
    public int currentTempBag = 0; // Current count of temporary items
    public Text scoreText; // UI component for displaying score
    public Text tempScoreText; // UI component for displaying temporary score
    public string OneScoreTag = "ItemSpawn"; // Tag for items worth one point
    public string ThreeScoreTag = "ScoreItems"; // Tag for items worth three points
    public string BadScoreTag = "PunishScoreItem"; // Tag for items that subtract points
    //public string BigerBag = "BigerBag";
    public string baseTag; // Base tag for depositing items


    //follow is for ?
    /*public GameObject speedRangeCollider; // Collider that increases player's speed
    public GameObject blueEffectPrefab; // Prefab for the visual effect when speed is increased
    float speedRangeDeactivationTime = 10.0f; // Time after which the speed range effect deactivates*/

    public GameObject Manager;
    private RoundsScript RoundsScript;
    private PlayerBase PlayerBase;
    public AudioSource audioSource;
    public int playerNo;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        RoundsScript = Manager.GetComponent<RoundsScript>();
        audioSource = GameObject.Find("UI Sounds").GetComponent<AudioSource>();
        PlayerBase = gameObject.GetComponent<PlayerBase>();
        playerNo = PlayerBase.playerNo;
        baseTag = "Home" + playerNo;
        string scoreTag = "Player" + playerNo + "Score";
       // scoreText = GameObject.FindGameObjectWithTag(scoreTag).GetComponent<Text>();
        string inventoryTag = "Player" + playerNo + "InventoryScore";
        //tempScoreText = GameObject.FindGameObjectWithTag(inventoryTag).GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(OneScoreTag) && currentTempBag < maxTempBag)
        {
            AddScore(other.gameObject, 1);
        }
        if (other.gameObject.CompareTag(ThreeScoreTag) && currentTempBag < maxTempBag)
        {
            AddScore(other.gameObject, 3);
        }
        /*if (other.gameObject.CompareTag(BigerBag))
        {
            maxTempBag = 5;
            Destroy(other.gameObject);
        }


        if (other.gameObject.CompareTag("chocolate"))
        {
            Destroy(other.gameObject);
            this.speedRangeCollider.gameObject.SetActive(true);
            GameObject newPrefab = Instantiate(blueEffectPrefab, this.transform.position, this.transform.rotation);
            newPrefab.transform.parent = this.transform;
            newPrefab.transform.localScale *= 10;
            StartCoroutine(DeactivateNodeAfterTime(newPrefab));
        }
        if (other.gameObject.CompareTag("speedRange"))
        {
            this.gameObject.GetComponent<PlayerBase>().setSpeed(true);
            StartCoroutine(recoverSpeed());
        }*/

        if (other.gameObject.CompareTag(BadScoreTag) && tempScore > 1)
        {
            BadFruitScore(other.gameObject);
        }
        else if (other.gameObject.CompareTag(baseTag))
        {
            ConvertTempScoreToScore();
        }
    }

    /*IEnumerator DeactivateNodeAfterTime(GameObject effect)
    {
        yield return new WaitForSeconds(speedRangeDeactivationTime);
        this.speedRangeCollider.gameObject.SetActive(false);
        Destroy(effect);
    }

    IEnumerator recoverSpeed()
    {
        yield return new WaitForSeconds(speedRangeDeactivationTime);
        this.gameObject.GetComponent<PlayerBase>().setSpeed(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("speedRange"))
        {
            this.gameObject.GetComponent<PlayerBase>().setSpeed(false);
        }
    }*/

    private void Update()
    {
        // Method could be used to continuously check conditions or implement logic to lose temporary score
    }


    private void AddScore(GameObject item, int scoreIncrease)
    {
        tempScore += scoreIncrease;
        currentTempBag++;
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        Destroy(item);
        UpdateTempScoreText();
    }


    private void BadFruitScore(GameObject item)
    {
        tempScore -= 1;

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        Destroy(item);
        UpdateTempScoreText();
    }


    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateTempScoreText()
    {
        if (tempScoreText != null)
        {
            tempScoreText.text = tempScore.ToString();
        }
    }

    private void ConvertTempScoreToScore()
    {
        score += tempScore; // Add temporary score to main score
        tempScore = 0; // Reset temporary score
        currentTempBag = 0;
        UpdateScoreText();
        UpdateTempScoreText();
        string scoreKey = "ScoreKey" + playerNo;
        PlayerPrefs.SetInt(scoreKey, score);
    }

    public void LoseTempScore()
    {
        // Logic for losing temporary score
        tempScore = 0;
        UpdateTempScoreText();
    }

    public void AddScore(int scoreToAdd)
    {
        tempScore += scoreToAdd;
        UpdateTempScoreText();
    }


}