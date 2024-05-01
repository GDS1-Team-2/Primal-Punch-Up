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
    public GameObject continueScreen2Players;
    public GameObject continueScreen3Players;
    public GameObject continueScreen4Players;

    public Image lizardIcon;
    public Image bearIcon;
    public Image foxIcon;
    public Image rabbitIcon;

    public GameObject lizardModel;
    public GameObject bearModel;
    public GameObject foxModel;
    public GameObject rabbitModel;

    public Image player1Icon;
    public Image player2Icon;
    public Image player3Icon;
    public Image player4Icon;

    public Slider player1Score;
    public Slider player2Score;
    public Slider player3Score;
    public Slider player4Score;

    //public Text winnerText;
    public Text FirstPlaceScoreText;
    public Text SecondPlaceScoreText;
    public Text ThirdPlaceScoreText;
    public int numberOfRounds;

    public Transform FirstPlacePosition;
    public Transform SecondPlacePosition;
    public Transform ThirdPlacePosition;

    public class WinsList
    {
        public int playerNumber;
        public int wins;
        public WinsList(int playerNumber, int score)
        {
            this.playerNumber = playerNumber;
            this.wins = score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("RoundNo") < 1)
        {
            switch (PlayerPrefs.GetInt("noOfPlayers"))
            {
                case 2:
                    continueScreen2Players.SetActive(true);
                    continueScreen3Players.SetActive(false);
                    continueScreen4Players.SetActive(false);
                    finalScreen.SetActive(false);

                    player1Score = GameObject.Find("Player 1 Score").GetComponent<Slider>();
                    player2Score = GameObject.Find("Player 2 Score").GetComponent<Slider>();

                    player1Icon = GameObject.Find("Player 1 Icon").GetComponent<Image>();
                    player2Icon = GameObject.Find("Player 2 Icon").GetComponent<Image>();

                    Player1ScoreText = GameObject.Find("Player1ScoreText").GetComponent<Text>();
                    Player2ScoreText = GameObject.Find("Player2ScoreText").GetComponent<Text>();

                    SetScore(player1Score, Player1ScoreText, PlayerPrefs.GetInt("Player1Wins"));
                    SetScore(player2Score, Player2ScoreText, PlayerPrefs.GetInt("Player2Wins"));

                    break;

                case 3:
                    continueScreen3Players.SetActive(true);
                    continueScreen2Players.SetActive(false);
                    continueScreen4Players.SetActive(false);
                    finalScreen.SetActive(false);

                    player1Score = GameObject.Find("Player 1 Score").GetComponent<Slider>();
                    player2Score = GameObject.Find("Player 2 Score").GetComponent<Slider>();
                    player3Score = GameObject.Find("Player 3 Score").GetComponent<Slider>();

                    player1Icon = continueScreen3Players.transform.Find("Player 1 Icon").GetComponent<Image>();
                    player2Icon = continueScreen3Players.transform.Find("Player 2 Icon").GetComponent<Image>();
                    player3Icon = continueScreen3Players.transform.Find("Player 3 Icon").GetComponent<Image>();

                    Player1ScoreText = continueScreen3Players.transform.Find("Player1ScoreText").GetComponent<Text>();
                    Player2ScoreText = continueScreen3Players.transform.Find("Player2ScoreText").GetComponent<Text>();
                    Player3ScoreText = continueScreen3Players.transform.Find("Player3ScoreText").GetComponent<Text>();

                    SetScore(player1Score, Player1ScoreText, PlayerPrefs.GetInt("Player1Wins"));
                    SetScore(player2Score, Player2ScoreText, PlayerPrefs.GetInt("Player2Wins"));
                    SetScore(player3Score, Player3ScoreText, PlayerPrefs.GetInt("Player3Wins"));

                    break;

                case 4:
                    continueScreen4Players.SetActive(true);
                    continueScreen2Players.SetActive(false);
                    continueScreen3Players.SetActive(false);
                    finalScreen.SetActive(false);

                    player1Score = GameObject.Find("Player 1 Score").GetComponent<Slider>();
                    player2Score = GameObject.Find("Player 2 Score").GetComponent<Slider>();
                    player3Score = GameObject.Find("Player 3 Score").GetComponent<Slider>();
                    player4Score = GameObject.Find("Player 4 Score").GetComponent<Slider>();

                    player1Icon = continueScreen4Players.transform.Find("Player 1 Icon").GetComponent<Image>();
                    player2Icon = continueScreen4Players.transform.Find("Player 2 Icon").GetComponent<Image>();
                    player3Icon = continueScreen4Players.transform.Find("Player 3 Icon").GetComponent<Image>();
                    player4Icon = continueScreen4Players.transform.Find("Player 4 Icon").GetComponent<Image>();

                    Player1ScoreText = continueScreen4Players.transform.Find("Player1ScoreText").GetComponent<Text>();
                    Player2ScoreText = continueScreen4Players.transform.Find("Player2ScoreText").GetComponent<Text>();
                    Player3ScoreText = continueScreen4Players.transform.Find("Player3ScoreText").GetComponent<Text>();
                    Player4ScoreText = continueScreen4Players.transform.Find("Player4ScoreText").GetComponent<Text>();

                    SetScore(player1Score, Player1ScoreText, PlayerPrefs.GetInt("Player1Wins"));
                    SetScore(player2Score, Player2ScoreText, PlayerPrefs.GetInt("Player2Wins"));
                    SetScore(player3Score, Player3ScoreText, PlayerPrefs.GetInt("Player3Wins"));
                    SetScore(player4Score, Player4ScoreText, PlayerPrefs.GetInt("Player4Wins"));

                    break;
            }

            SetIcon(player1Icon, PlayerPrefs.GetString("Player1Model"));
            SetIcon(player2Icon, PlayerPrefs.GetString("Player2Model"));

            if (PlayerPrefs.GetString("Player3Model") != null)
            {
                SetIcon(player3Icon, PlayerPrefs.GetString("Player3Model"));
            }
            if (PlayerPrefs.GetString("Player4Model") != null)
            {
                SetIcon(player4Icon, PlayerPrefs.GetString("Player4Model"));
            }
        }
        else {
            finalScreen.SetActive(true);
            continueScreen2Players.SetActive(false);
            continueScreen3Players.SetActive(false);
            continueScreen4Players.SetActive(false);

            CalculateWinner();
        }
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

    public void SetModel(Vector3 pos, string animal)
    {
        switch (animal)
        {
            case "Lizard":
                lizardModel.transform.position = pos;
                break;
            case "Bear":
                bearModel.transform.position = pos;
                break;
            case "Fox":
                foxModel.transform.position = pos;
                break;
            case "Rabbit":
                rabbitModel.transform.position = pos;
                break;
        }
    }

    public void SetScore(Slider slider, Text scoreText, int scoreValue)
    {
        //0 at -162
        //1 at -38
        //2 at 172
        //3 at 385
        //slider.value = scoreValue;
        scoreText.text = scoreValue.ToString();
        StartCoroutine(IncreaseScore(slider, slider.value, scoreValue, 1));

        /*switch (scoreValue)
        {
            case 0:
                scoreText.gameObject.transform.position = new Vector3(-162, scoreText.gameObject.transform.position.y, scoreText.gameObject.transform.position.z);
                break;
            case 1:
                scoreText.rectTransform.position = new Vector3(-138, scoreText.rectTransform.position.y, scoreText.rectTransform.position.z);
                break;
            case 2:
                scoreText.rectTransform.position = new Vector3(172, scoreText.rectTransform.position.y, scoreText.rectTransform.position.z);
                break;
            case 4:
                scoreText.rectTransform.position = new Vector3(385, scoreText.rectTransform.position.y, scoreText.rectTransform.position.z);
                break;
        }*/
    }

    private IEnumerator IncreaseScore(Slider slider, float startValue, int endValue, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(startValue, endValue, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void CalculateWinner()
    {
        List<int> wins = new List<int>();
        int[] playerindex = {1, 2, 3, 4};

        wins.Add(PlayerPrefs.GetInt("Player1Wins"));
        wins.Add(PlayerPrefs.GetInt("Player2Wins"));
        wins.Add(PlayerPrefs.GetInt("Player3Wins"));
        wins.Add(PlayerPrefs.GetInt("Player4Wins"));

        for (int i = 1; i < 4; ++i)
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
            string playerno = "Player" + playerindex[i] + "Model";
            switch (i)
            {
                case 0:
                    FirstPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    SetModel(FirstPlacePosition.position, PlayerPrefs.GetString(playerno));
                    break;
                case 1:
                    SecondPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    SetModel(SecondPlacePosition.position, PlayerPrefs.GetString(playerno));
                    break;
                case 2:
                    ThirdPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    SetModel(ThirdPlacePosition.position, PlayerPrefs.GetString(playerno));
                    break;
            }
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
