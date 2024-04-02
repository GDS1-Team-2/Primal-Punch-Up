using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;
    public Text player1score;
    public Text player2score;
    public int score1;
    public int score2;

    private bool load = false;

    void Start()
    {
        TimerOn = true;
        DontDestroyOnLoad(this.gameObject);
       // score1 = GameObject.FindGameObjectWithTag("Lizard").GetComponent<PickupItem>().score;
        //score1 = GameObject.FindGameObjectWithTag("Bear").GetComponent<PickupItem>().score;
    }

    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            UpdateTimer(TimeLeft);
        }
        else
        {
            TimeLeft = 0;
            TimerOn = false;
            if (!load)
            {
                //score1 = GameObject.FindGameObjectWithTag("Lizard").GetComponent<PickupItem>().score;
                //score1 = GameObject.FindGameObjectWithTag("Bear").GetComponent<PickupItem>().score;
                SceneManager.LoadScene("Complete");
                load = true;
                //player1score = GameObject.FindGameObjectWithTag("score1").GetComponent<Text>();
                //player2score = GameObject.FindGameObjectWithTag("score2").GetComponent<Text>();
                //player1score.text = score1.ToString();
                //player2score.text = score2.ToString();
            }
        }


    }

    void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}
