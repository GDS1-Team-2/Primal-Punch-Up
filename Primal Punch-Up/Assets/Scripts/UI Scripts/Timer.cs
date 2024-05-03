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

    public Text TimerText2;
    public Text TimerText3;
    public Text TimerText4;

    private RoundsScript RoundsScript;

    private bool load = false;

    void Start()
    {
        TimerOn = true;
        RoundsScript = gameObject.GetComponent<RoundsScript>();
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

        TimerText2.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimerText3.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimerText4.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}
