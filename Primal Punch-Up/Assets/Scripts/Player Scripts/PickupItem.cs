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
    //public int tempScore = 0; // Player's temporary score
    //public int maxTempBag = 3; // Maximum capacity of temporary items, can be modified in Unity
    //public int currentTempBag = 0; // Current count of temporary items
    public Text scoreText; // UI component for displaying score
    //public Text tempScoreText; // UI component for displaying temporary score
    public string OneScoreTag; // Tag for items worth one point
    public string MultiScoreTag; // Tag for items worth three points
    public string BadScoreTag; // Tag for items that subtract points
    //public string BiggerBag = "BiggerBag";
    //public string baseTag; // Base tag for depositing items


    //follow is for ?
    /*public GameObject speedRangeCollider; // Collider that increases player's speed
    public GameObject blueEffectPrefab; // Prefab for the visual effect when speed is increased
    float speedRangeDeactivationTime = 10.0f; // Time after which the speed range effect deactivates*/

    public GameObject Manager;
    private RoundsScript RoundsScript;
    private PlayerBase PlayerBase;
    public AudioSource audioSource;
    public int playerNo;

    public bool canPickup;

    void Start()
    {
        canPickup = true;
        Manager = GameObject.FindGameObjectWithTag("Manager");
        RoundsScript = Manager.GetComponent<RoundsScript>();
        audioSource = GameObject.Find("UI Sounds").GetComponent<AudioSource>();
        PlayerBase = gameObject.GetComponent<PlayerBase>();
        playerNo = PlayerBase.playerNo;
        string scoreTag = "Player" + playerNo + "Score";
        scoreText = GameObject.FindGameObjectWithTag(scoreTag).GetComponent<Text>();
        //string inventoryTag = "Player" + playerNo + "InventoryScore";
        //tempScoreText = GameObject.FindGameObjectWithTag(inventoryTag).GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canPickup)
        {
            if (other.gameObject.CompareTag(OneScoreTag))
            {
                AddScore(other.gameObject, 1);
            }
            if (other.gameObject.CompareTag(MultiScoreTag))
            {
                AddScore(other.gameObject, 3);
            }
            if (other.gameObject.CompareTag(BadScoreTag))
            {
                SubtractScore(other.gameObject);
            }
        }
        
    }


    public void AddScore(GameObject item, int scoreIncrease)
    {
        score += scoreIncrease;
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        Destroy(item);
        UpdateScoreText();
        string scoreKey = "ScoreKey" + PlayerBase.playerNo;
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.Save();
    }


    public void SubtractScore(GameObject item)
    {
        score -= 1;
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        Destroy(item);
        UpdateScoreText();
    }

    public void DropScoreOnDeath(int number)
    {
        score -= number;
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

}