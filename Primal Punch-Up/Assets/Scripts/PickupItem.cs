using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public AudioClip pickupSound; // Sound effect for picking up items

    public int score = 0; // Player's score
    public int tempScore = 0; // Player's temporary score
    public int maxTempBag = 3; // Maximum temporary item capacity, can be modified in Unity
    public int currentTempBag = 0; // Current count of temporary items
    public Text scoreText; // UI component for displaying score
    public Text tempScoreText; // UI component for displaying temporary score
    public string OneScoreTag; // Tag for items worth one point
    public string ThreeScoreTag; // Tag for items worth three points
    public string BadScoreTag; // Tag for items that subtract points
    public string baseTag; // Base tag for depositing items

    public GameObject speedRangeCollider; // Collider that increases player's speed

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(OneScoreTag) && currentTempBag < maxTempBag)
        {
            // Increase temporary score
            tempScore++;
            currentTempBag++;
            // Play pickup sound effect
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            // Destroy the item object
            Destroy(other.gameObject);
            // Update UI to display temporary score
            UpdateTempScoreText();
        }
        if (other.gameObject.CompareTag(ThreeScoreTag) && currentTempBag < maxTempBag)
        {
            // Increase temporary score
            tempScore += 3;
            currentTempBag++;
            // Play pickup sound effect
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            // Destroy the item object
            Destroy(other.gameObject);
            // Update UI to display temporary score
            UpdateTempScoreText();
        }

        /*if (other.gameObject.CompareTag("chocolate"))
        {
            Destroy(other.gameObject);
            this.speedRangeCollider.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("speedRange"))
        {
            this.gameObject.GetComponent<PlayerBase>().setSpeed(true);
        }*/

        if (other.gameObject.CompareTag(BadScoreTag) && tempScore > 1)
        {
            // Decrease temporary score
            tempScore -= 1;

            // Play pickup sound effect
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            // Destroy the item object
            Destroy(other.gameObject);
            // Update UI to display temporary score
            UpdateTempScoreText();
        }
        else if (other.gameObject.CompareTag(baseTag))
        {
            // Convert temporary score to permanent score
            ConvertTempScoreToScore();
        }

        // Check if the level completion condition is met
        // CheckForLevelCompletion();
    }

    private void Update()
    {
        // Continuously check for conditions such as losing temporary score, etc.
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

    private void CheckForLevelCompletion()
    {
        if (score >= 11) // Set the condition for level completion based on requirements
        {
            // Load the "PassScene" or similar
            // SceneManager.LoadScene("PassScene");
        }
    }

    private void ConvertTempScoreToScore()
    {
        score += tempScore; // Add temporary score to main score
        tempScore = 0; // Reset temporary score
        currentTempBag = 0; // Reset temporary item count
        UpdateScoreText();
        UpdateTempScoreText();
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