using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteScoreScript : MonoBehaviour
{
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        Player1ScoreText.text = PlayerPrefs.GetInt("Player1Wins").ToString();
        Player2ScoreText.text = PlayerPrefs.GetInt("Player2Wins").ToString();
        Player3ScoreText.text = PlayerPrefs.GetInt("Player3Wins").ToString();
        Player4ScoreText.text = PlayerPrefs.GetInt("Player4Wins").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
