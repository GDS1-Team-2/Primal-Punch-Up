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
    private bool countdown = false;

    public GameObject timer1;
    public GameObject timer2;
    public GameObject timer3;
    public Text TimerText2;
    public Text TimerText3;
    public Text TimerText4;

    private RoundsScript RoundsScript;

    private bool load = false;

    void Start()
    {
        TimerOn = true;
        RoundsScript = gameObject.GetComponent<RoundsScript>();
        countdown = false;
    }

    void Update()
    {
        if (TimeLeft > 1)
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
                RoundsScript.EndRound();
                load = true;
            }
        }


    }

    void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        if (countdown == false && seconds < 5.5)
        {
            countdown = true;
            TimerText2.color = Color.red;
            TimerText3.color = Color.red;
            TimerText4.color = Color.red;
            timer1.GetComponent<Animator>().Play("Countdown");
            timer2.GetComponent<Animator>().Play("Countdown");
            timer3.GetComponent<Animator>().Play("Countdown");
        }

        TimerText2.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimerText3.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimerText4.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}
