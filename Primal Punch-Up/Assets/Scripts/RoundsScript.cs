using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundsScript : MonoBehaviour
{
    public string Map1SceneName;
    public string Map2SceneName;
    public string Map3SceneName;
    public string CompleteSceneName;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject CharacterLoader;
    public GameObject UICanvas2Players;
    public GameObject UICanvas3Players;
    public GameObject UICanvas4Players;

    public bool newRound = false;
    

    // Start is called before the first frame update
    void Start()
    {
        CharacterLoader = GameObject.Find("CharacterLoader");
        CharacterLoader.GetComponent<CharacterLoader>().loadCharacters = true;
        int noOfPlayers = CharacterLoader.GetComponent<CharacterLoader>().noOfPlayers;

        UICanvas2Players = GameObject.Find("UICanvas2Players");
        UICanvas3Players = GameObject.Find("UICanvas3Players");
        UICanvas4Players = GameObject.Find("UICanvas4Players");

        switch (noOfPlayers)
        {
            case 2:
                UICanvas3Players.SetActive(false);
                UICanvas4Players.SetActive(false);
                UICanvas2Players.SetActive(true);
                break;
            case 3:
                UICanvas2Players.SetActive(false);
                UICanvas4Players.SetActive(false);
                UICanvas3Players.SetActive(true);
                break;
            case 4:
                UICanvas2Players.SetActive(false);
                UICanvas3Players.SetActive(false);
                UICanvas4Players.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (newRound)
        {
            PlayerPrefs.SetInt("ScoreKey1", 0);
            PlayerPrefs.SetInt("ScoreKey2", 0);
            PlayerPrefs.SetInt("ScoreKey3", 0);
            PlayerPrefs.SetInt("ScoreKey4", 0);
        }
    }

    public void SetPlayer1(GameObject player)
    {
        Player1 = player;
    }

    public void SetPlayer2(GameObject player)
    {
        Player2 = player;
    }

    public void SetPlayer3(GameObject player)
    {
        Player3 = player;
    }

    public void SetPlayer4(GameObject player)
    {
        Player4 = player;
    }

    public void SavePlayer1Score(int score)
    {
        PlayerPrefs.SetInt("ScoreKey1", score);
        PlayerPrefs.Save();
    }
    public void SavePlayer2Score(int score)
    {
        PlayerPrefs.SetInt("ScoreKey2", score);
        PlayerPrefs.Save();
    }
    public void SavePlayer3Score(int score)
    {
        PlayerPrefs.SetInt("ScoreKey3", score);
        PlayerPrefs.Save();
    }
    public void SavePlayer4Score(int score)
    {
        PlayerPrefs.SetInt("ScoreKey4", score);
        PlayerPrefs.Save();
    }

    public void EndRound()
    {
        int[] scores = {PlayerPrefs.GetInt("ScoreKey1"), PlayerPrefs.GetInt("ScoreKey2"),
                        PlayerPrefs.GetInt("ScoreKey3"), PlayerPrefs.GetInt("ScoreKey4")};

        int maxIndex = -1;
        int maxValue = 0;
        for (int i = 0; i < 4; i++)
        {
            if (scores[i] > maxValue) 
            {
                maxValue = scores[i];
                maxIndex = i;
            }
        }
        string winner = "Player" + (maxIndex+1) + "Wins";
        PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("RoundNo", (PlayerPrefs.GetInt("RoundNo")+1));
        SceneManager.LoadScene(CompleteSceneName);
    }
}
