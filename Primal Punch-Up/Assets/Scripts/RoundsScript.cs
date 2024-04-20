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

    // Start is called before the first frame update
    void Start()
    {
       // Player1 = GameObject.FindGameObjectWithTag()
    }

    // Update is called once per frame
    void Update()
    {
        
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
        string winner = "Player" + maxIndex + "Wins";
        PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
        SceneManager.LoadScene(CompleteSceneName);
    }
}
