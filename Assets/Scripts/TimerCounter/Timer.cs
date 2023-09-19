using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float timeValue = 0;

    [Header("Component")]
    public TextMeshProUGUI finalTimer;


    public Knight knight;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!knight.isDead)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
            finalTimer.text = timeValue.ToString();
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 1000;

        timerText.text = "Time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void DisplayTime(float timeToDisplay, TextMeshProUGUI timeText)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 1000;

        timeText.text = "Best Time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
