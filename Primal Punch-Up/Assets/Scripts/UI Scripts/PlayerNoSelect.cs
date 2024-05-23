using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNoSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nextBtn;

    public void ActivateButton()
    {
        if (PlayerPrefs.GetInt("noOfRounds") != 0 && PlayerPrefs.GetInt("noOfPlayers") != 0)
        {
            nextBtn.SetActive(true);
        }
    }
}
