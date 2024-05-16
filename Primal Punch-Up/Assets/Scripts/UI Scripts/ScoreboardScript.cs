using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardScript : MonoBehaviour
{
    private Text Player1ScoreText;
    private Text Player2ScoreText;
    private Text Player3ScoreText;
    private Text Player4ScoreText;

    public GameObject continueScreen2Players;
    public GameObject continueScreen3Players;
    public GameObject continueScreen4Players;

    public Image lizardIcon;
    public Image bearIcon;
    public Image foxIcon;
    public Image rabbitIcon;

    private Image player1Icon;
    private Image player2Icon;
    private Image player3Icon;
    private Image player4Icon;

    private Slider player1Score;
    private Slider player2Score;
    private Slider player3Score;
    private Slider player4Score;

    public float dropDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        continueScreen2Players.SetActive(false);
        continueScreen3Players.SetActive(false);
        continueScreen4Players.SetActive(false);
    }

    public void ScoreBoard()
    {
        switch (PlayerPrefs.GetInt("noOfPlayers"))
        {
            case 2:
                continueScreen2Players.SetActive(true);
                continueScreen3Players.SetActive(false);
                continueScreen4Players.SetActive(false);

                player1Score = GameObject.Find("Player 1 Score").GetComponent<Slider>();
                player2Score = GameObject.Find("Player 2 Score").GetComponent<Slider>();

                player1Icon = GameObject.Find("Player 1 Icon").GetComponent<Image>();
                player2Icon = GameObject.Find("Player 2 Icon").GetComponent<Image>();

                Player1ScoreText = GameObject.Find("Player1ScoreText").GetComponent<Text>();
                Player2ScoreText = GameObject.Find("Player2ScoreText").GetComponent<Text>();

                SetIcon(player1Icon, PlayerPrefs.GetString("Player1Model"));
                SetIcon(player2Icon, PlayerPrefs.GetString("Player2Model"));

                StartCoroutine(ScoreboardDrop(continueScreen2Players, dropDuration, 2));

                break;

            case 3:
                continueScreen3Players.SetActive(true);
                continueScreen2Players.SetActive(false);
                continueScreen4Players.SetActive(false);

                player1Score = GameObject.Find("Player 1 Score").GetComponent<Slider>();
                player2Score = GameObject.Find("Player 2 Score").GetComponent<Slider>();
                player3Score = GameObject.Find("Player 3 Score").GetComponent<Slider>();

                player1Icon = GameObject.Find("Player 1 Icon").GetComponent<Image>();
                player2Icon = GameObject.Find("Player 2 Icon").GetComponent<Image>();
                player3Icon = GameObject.Find("Player 3 Icon").GetComponent<Image>();

                Player1ScoreText = GameObject.Find("Player1ScoreText").GetComponent<Text>();
                Player2ScoreText = GameObject.Find("Player2ScoreText").GetComponent<Text>();
                Player3ScoreText = GameObject.Find("Player3ScoreText").GetComponent<Text>();

                SetIcon(player1Icon, PlayerPrefs.GetString("Player1Model"));
                SetIcon(player2Icon, PlayerPrefs.GetString("Player2Model"));
                SetIcon(player3Icon, PlayerPrefs.GetString("Player3Model"));

                StartCoroutine(ScoreboardDrop(continueScreen3Players, dropDuration, 3));

                break;

            case 4:
                continueScreen4Players.SetActive(true);
                continueScreen2Players.SetActive(false);
                continueScreen3Players.SetActive(false);

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

                SetIcon(player1Icon, PlayerPrefs.GetString("Player1Model"));
                SetIcon(player2Icon, PlayerPrefs.GetString("Player2Model"));
                SetIcon(player3Icon, PlayerPrefs.GetString("Player3Model"));
                SetIcon(player4Icon, PlayerPrefs.GetString("Player4Model"));

                StartCoroutine(ScoreboardDrop(continueScreen4Players, dropDuration, 4));

                break;
        }
    }

    public IEnumerator ScoreboardDrop(GameObject canvas, float duration, int players)
    {
        Vector3 startPos = new Vector3(canvas.transform.position.x, 1700, canvas.transform.position.z);
        Vector3 endPos = new Vector3(canvas.transform.position.x, 540, canvas.transform.position.z);
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            canvas.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetScore(player1Score, Player1ScoreText, PlayerPrefs.GetInt("Player1Wins"));
        SetScore(player2Score, Player2ScoreText, PlayerPrefs.GetInt("Player2Wins"));
        if (players == 3)
        {
            SetScore(player3Score, Player3ScoreText, PlayerPrefs.GetInt("Player3Wins"));
        }
        else if (players == 4)
        {
            SetScore(player3Score, Player3ScoreText, PlayerPrefs.GetInt("Player3Wins"));
            SetScore(player4Score, Player4ScoreText, PlayerPrefs.GetInt("Player4Wins"));
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


    // Update is called once per frame
    void Update()
    {

    }

}
