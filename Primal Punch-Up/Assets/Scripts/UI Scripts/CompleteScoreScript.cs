using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteScoreScript : MonoBehaviour
{
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;
    public GameObject finalScreen;
    public GameObject continueScreen;
    public Text winnerText;
    public Text winnerScoreText;
    public int numberOfRounds;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("RoundNo") < 4)
        {
            continueScreen.SetActive(true);
            finalScreen.SetActive(false);

            Player1ScoreText.text = PlayerPrefs.GetInt("Player1Wins").ToString();
            Player2ScoreText.text = PlayerPrefs.GetInt("Player2Wins").ToString();
            Player3ScoreText.text = PlayerPrefs.GetInt("Player3Wins").ToString();
            Player4ScoreText.text = PlayerPrefs.GetInt("Player4Wins").ToString();
        }
        else {
            finalScreen.SetActive(true);
            continueScreen.SetActive(false);

            int[] wins = {PlayerPrefs.GetInt("Player1Wins"),
                            PlayerPrefs.GetInt("Player2Wins"),
                            PlayerPrefs.GetInt("Player3Wins"),
                            PlayerPrefs.GetInt("Player4Wins")};

            int maxIndex = -1;
            int maxValue = 0;
            for (int i = 0; i < 4; i++)
            {
                if (wins[i] > maxValue)
                {
                    maxValue = wins[i];
                    maxIndex = i;
                }
            }

            string winner = "Player " + (maxIndex + 1);
            winnerText.text = winner;
            string winscore = "Player" + (maxIndex + 1) + "Wins";
            winnerScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
