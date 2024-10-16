using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteScoreScript : MonoBehaviour
{
    public Image lizardIcon;
    public Image bearIcon;
    public Image foxIcon;
    public Image rabbitIcon;

    public GameObject[] lizardModels;
    public GameObject[] bearModels;
    public GameObject[] foxModels;
    public GameObject[] rabbitModels;

    public Text FirstPlaceScoreText;
    public Text SecondPlaceScoreText;
    public Text ThirdPlaceScoreText;

    public Transform FirstPlacePosition;
    public Transform SecondPlacePosition;
    public Transform ThirdPlacePosition;

    public Image FirstPlaceIcon;
    public Image SecondPlaceIcon;
    public Image ThirdPlaceIcon;

    public Material Player1Outline;
    public Material Player2Outline;
    public Material Player3Outline;
    public Material Player4Outline;

    void Start()
    {
        CalculateWinner();
    }

    public void SetIcon(Image playerIcon, string animal)
    {
        switch (animal)
        {
            case "Lizard":
                playerIcon.sprite = lizardIcon.sprite;
                break;
            case "Bear":
                playerIcon.sprite = bearIcon.sprite;
                break;
            case "Fox":
                playerIcon.sprite = foxIcon.sprite;
                break;
            case "Rabbit":
                playerIcon.sprite = rabbitIcon.sprite;
                break;
        }
    }

    public GameObject[] SelectAnimal(string animal)
    {
        switch (animal)
        {
            case "Lizard":
                return lizardModels;
            case "Bear":
                return bearModels;
            case "Fox":
                return foxModels;
            case "Rabbit":
                return rabbitModels;
        }
        return lizardModels; // Default case
    }

    public void CalculateWinner()
    {
        List<int> wins = new List<int>();
        int[] playerindex = { 1, 2, 3, 4 };

        wins.Add(PlayerPrefs.GetInt("Player1Wins"));
        wins.Add(PlayerPrefs.GetInt("Player2Wins"));
        wins.Add(PlayerPrefs.GetInt("Player3Wins"));
        wins.Add(PlayerPrefs.GetInt("Player4Wins"));

        for (int i = 1; i < wins.Count; ++i)
        {
            int key = wins[i];
            int keyIndex = playerindex[i];
            int j = i - 1;

            while (j >= 0 && wins[j] < key)
            {
                wins[j + 1] = wins[j];
                playerindex[j + 1] = playerindex[j];
                j = j - 1;
            }
            wins[j + 1] = key;
            playerindex[j + 1] = keyIndex;
        }

        for (int i = 0; i < 3; i++)
        {
            string winscore = "Player" + playerindex[i] + "Wins";
            string playermodel = "Player" + playerindex[i] + "Model";
            switch (i)
            {
                case 0:
                    FirstPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    GameObject model1 = SelectAnimal(PlayerPrefs.GetString(playermodel))[0];
                    model1.transform.position = FirstPlacePosition.position;
                    SetIcon(FirstPlaceIcon, PlayerPrefs.GetString(playermodel));
                    SetIconColour(FirstPlaceIcon, playerindex[i]);
                    model1.GetComponent<Animator>().Play(PlayerPrefs.GetString(playermodel) + "Victory");
                    break;
                case 1:
                    SecondPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    GameObject model2 = SelectAnimal(PlayerPrefs.GetString(playermodel))[1]; // Use the same model index as the first place
                    model2.transform.position = SecondPlacePosition.position;
                    SetIcon(SecondPlaceIcon, PlayerPrefs.GetString(playermodel));
                    SetIconColour(SecondPlaceIcon, playerindex[i]);
                    model2.GetComponent<Animator>().Play(PlayerPrefs.GetString(playermodel) + "Defeat");
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("noOfPlayers") > 2)
                    {
                        ThirdPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                        GameObject model3 = SelectAnimal(PlayerPrefs.GetString(playermodel))[2]; // Use the same model index as the first place
                        model3.transform.position = ThirdPlacePosition.position;
                        SetIcon(ThirdPlaceIcon, PlayerPrefs.GetString(playermodel));
                        SetIconColour(ThirdPlaceIcon, playerindex[i]);
                        model3.GetComponent<Animator>().Play(PlayerPrefs.GetString(playermodel) + "Defeat");
                    }
                    else
                    {
                        ThirdPlaceIcon.enabled = false;
                        ThirdPlaceScoreText.enabled = false;
                    }
                    break;
            }
        }

        // Check if there are only two players and hide third place if true
        if (PlayerPrefs.GetInt("noOfPlayers") == 2)
        {
            ThirdPlaceIcon.enabled = false;
            ThirdPlaceScoreText.enabled = false;
        }
    }

    public void SetIconColour(Image playerIcon, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                playerIcon.material = Player1Outline;
                break;
            case 2:
                playerIcon.material = Player2Outline;
                break;
            case 3:
                playerIcon.material = Player3Outline;
                break;
            case 4:
                playerIcon.material = Player4Outline;
                break;
        }
    }

    void Update()
    {

    }
}