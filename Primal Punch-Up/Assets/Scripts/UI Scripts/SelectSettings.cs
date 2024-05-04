using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectSettings : MonoBehaviour
{
    public int noOfPlayers = 1;
    public int noOfRounds = 1;

    public GameObject playerDrop;
    public TMP_Dropdown playerNoDrop;

    public GameObject roundDrop;
    public TMP_Dropdown roundNoDrop;

    // Start is called before the first frame update
    void Start()
    {
        playerDrop = GameObject.Find("NoPlayersDropDown");
        playerNoDrop = playerDrop.GetComponent<TMP_Dropdown>();
        //roundDrop = GameObject.Find("NoRoundsDropDown");
        //roundNoDrop = roundDrop.GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void UpdatePlayerCountDropdown()
    {
        string selectedOptionText = playerNoDrop.options[playerNoDrop.value].text;

        if (int.TryParse(selectedOptionText, out int result))
        {
            noOfPlayers = result;
            PlayerPrefs.SetInt("noOfPlayers", noOfPlayers);
            PlayerPrefs.Save();
            //print(noOfPlayers);
        }
        else
        {
            Debug.LogWarning("Failed to parse player count.");
        }
    }

    public void UpdateRoundCount()
    {
        string selectedOptionText = roundNoDrop.options[roundNoDrop.value].text;

        if (int.TryParse(selectedOptionText, out int result))
        {
            noOfRounds = result;
            PlayerPrefs.SetInt("numberOfRounds", noOfRounds);
            PlayerPrefs.Save();
            //print(noOfRounds);
        }
        else
        {
            Debug.LogWarning("Failed to parse round count.");
        }
    }
}
