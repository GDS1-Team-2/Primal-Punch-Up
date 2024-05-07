using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteScoreScript : MonoBehaviour
{
    private Text Player1ScoreText;
    private Text Player2ScoreText;
    private Text Player3ScoreText;
    private Text Player4ScoreText;

    public GameObject finalScreen;
    public GameObject continueScreen2Players;
    public GameObject continueScreen3Players;
    public GameObject continueScreen4Players;

    public Image lizardIcon;
    public Image bearIcon;
    public Image foxIcon;
    public Image rabbitIcon;

    public GameObject[] lizardModels;
    public GameObject[] bearModels;
    public GameObject[] foxModels;
    public GameObject[] rabbitModels;

    private Image player1Icon;
    private Image player2Icon;
    private Image player3Icon;
    private Image player4Icon;

    private Slider player1Score;
    private Slider player2Score;
    private Slider player3Score;
    private Slider player4Score;

    //public Text winnerText;
    public Text FirstPlaceScoreText;
    public Text SecondPlaceScoreText;
    public Text ThirdPlaceScoreText;
    //public int numberOfRounds;

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

                    player1Icon = GameObject.Find("Player 1 Icon").GetComponent<Image>();
                    player2Icon = GameObject.Find("Player 2 Icon").GetComponent<Image>();
                    player3Icon = GameObject.Find("Player 3 Icon").GetComponent<Image>();

                    Player1ScoreText = GameObject.Find("Player1ScoreText").GetComponent<Text>();
                    Player2ScoreText = GameObject.Find("Player2ScoreText").GetComponent<Text>();
                    Player3ScoreText = GameObject.Find("Player3ScoreText").GetComponent<Text>();

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

                    player1Icon = GameObject.Find("Player 1 Icon").GetComponent<Image>();
                    player2Icon = GameObject.Find("Player 2 Icon").GetComponent<Image>();
                    player3Icon = GameObject.Find("Player 3 Icon").GetComponent<Image>();
                    player4Icon = GameObject.Find("Player 4 Icon").GetComponent<Image>();

                    Player1ScoreText = GameObject.Find("Player1ScoreText").GetComponent<Text>();
                    Player2ScoreText = GameObject.Find("Player2ScoreText").GetComponent<Text>();
                    Player3ScoreText = GameObject.Find("Player3ScoreText").GetComponent<Text>();
                    Player4ScoreText = GameObject.Find("Player4ScoreText").GetComponent<Text>();

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

    //public void SetModel(Vector3 pos, string animal, int number)
   // {
    //    SelectAnimal(animal)[number].transform.position = pos;
    //}

    public GameObject[] SelectAnimal(string animal)
    {
        switch (animal)
        {
            case "Lizard":
                return lizardModels;
                break;
            case "Bear":
                return bearModels;
                break;
            case "Fox":
                return foxModels;
                break;
            case "Rabbit":
                return rabbitModels;
                break;
        }
        return lizardModels;
    }

    /*public void SetAnim(GameObject model, string state)
    {
        string anim = animal + state;
        Debug.Log(anim);
        switch (animal)
        {
            case "Lizard":
                lizardModel
                break;
            case "Bear":
                bearModel.GetComponent<Animator>().Play(anim);
                break;
            case "Fox":
                foxModel.GetComponent<Animator>().Play(anim);
                break;
            case "Rabbit":
                rabbitModel.GetComponent<Animator>().Play(anim);
                break;
        }
    }*/

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
            string playermodel = "Player" + playerindex[i] + "Model";
            switch (i)
            {
                case 0:
                    FirstPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    GameObject model1 = SelectAnimal(PlayerPrefs.GetString(playermodel))[1];
                    model1.transform.position = FirstPlacePosition.position;
                    SetIcon(FirstPlaceIcon, PlayerPrefs.GetString(playermodel));
                    SetIconColour(FirstPlaceIcon, playerindex[i]);
                    model1.GetComponent<Animator>().Play(PlayerPrefs.GetString(playermodel) + "Victory");
                    break;
                case 1:
                    SecondPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                    GameObject model2 = SelectAnimal(PlayerPrefs.GetString(playermodel))[2];
                    model2.transform.position = SecondPlacePosition.position;
                    SetIcon(SecondPlaceIcon, PlayerPrefs.GetString(playermodel));
                    SetIconColour(SecondPlaceIcon, playerindex[i]);
                    model2.GetComponent<Animator>().Play(PlayerPrefs.GetString(playermodel) + "Defeat");
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("noOfPlayers") > 2)
                    {
                        ThirdPlaceScoreText.text = PlayerPrefs.GetInt(winscore).ToString();
                        GameObject model3 = SelectAnimal(PlayerPrefs.GetString(playermodel))[3];
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


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
