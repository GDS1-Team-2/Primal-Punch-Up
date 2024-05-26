using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    //follow is for pickup score item
    public AudioClip pickupSound; // Sound effect for picking up items
    public AudioClip badSound;
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
    public PickupUIScript PickupUIScript;
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
        PickupUIScript = gameObject.GetComponent<PickupUIScript>();
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
                SubtractScore(other.gameObject, 3);
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
        for (int i = 0; i < scoreIncrease; i++)
        {
            PickupUIScript.PopUp("plus", scoreIncrease);
        }
            
        UpdateScoreText();
        string scoreKey = "ScoreKey" + PlayerBase.playerNo;
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.Save();
    }


    public void SubtractScore(GameObject item, int scoreDecrease)
    {
        if (score-scoreDecrease < 0)
        {
            score = 0;
        }
        else
        {
            score -= scoreDecrease;
        }

        if (badSound != null)
        {
            audioSource.PlayOneShot(badSound);
        }
        Destroy(item);

        for (int i = 0; i < scoreDecrease; i++)
        {
            PickupUIScript.PopUp("minus", scoreDecrease);
        }

        UpdateScoreText();
        string scoreKey = "ScoreKey" + PlayerBase.playerNo;
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.Save();
    }

    public int CurrentScore()
    {
        return score;
    }

    public void DropScoreOnDeath(int number)
    {
        if (number <= score)
        {
            score -= number;
        }
        else
        {
            score = 0;
        }
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