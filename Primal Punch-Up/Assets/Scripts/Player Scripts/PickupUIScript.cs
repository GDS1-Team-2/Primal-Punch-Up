using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject twoP1plus;
    public GameObject twoP1minus;
    public GameObject twoP2plus;
    public GameObject twoP2minus;
    public GameObject twoP1multi;
    public GameObject twoP2multi;

    public GameObject fourP1plus;
    public GameObject fourP1minus;
    public GameObject fourP2plus;
    public GameObject fourP2minus;
    public GameObject fourP3plus;
    public GameObject fourP3minus;
    public GameObject fourP4plus;
    public GameObject fourP4minus;
    public GameObject fourP1multi;
    public GameObject fourP2multi;
    public GameObject fourP3multi;
    public GameObject fourP4multi;

    private int playerNo;

    public void SetPlayerNo(int number)
    {
        playerNo = number;
    }


    public void PopUp(string state, int score)
    {
        if (PlayerPrefs.GetInt("noOfPlayers") == 2)
        {
            if (playerNo == 1)
            {
                if (state == "plus") 
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(twoP1plus);
                            break;
                        case 3:
                            Instantiate(twoP1multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(twoP1minus); }
            }
            else if (playerNo == 2)
            {
                if (state == "plus")
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(twoP2plus);
                            break;
                        case 3:
                            Instantiate(twoP2multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(twoP2minus); }
            }
        }
        else
        {
            if (playerNo == 1)
            {
                if (state == "plus")
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(fourP1plus);
                            break;
                        case 3:
                            Instantiate(fourP1multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(fourP1minus); }
            }
            else if (playerNo == 2)
            {
                if (state == "plus")
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(fourP2plus);
                            break;
                        case 3:
                            Instantiate(fourP2multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(fourP2minus); }
            }
            else if (playerNo == 3)
            {
                if (state == "plus")
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(fourP3plus);
                            break;
                        case 3:
                            Instantiate(fourP3multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(fourP3minus); }
            }
            else if (playerNo == 4)
            {
                if (state == "plus")
                {
                    switch (score)
                    {
                        case 1:
                            Instantiate(fourP4plus);
                            break;
                        case 3:
                            Instantiate(fourP4multi);
                            break;
                    }
                }
                else if (state == "minus") { Instantiate(fourP4minus); }
            }
        }
    }
}
