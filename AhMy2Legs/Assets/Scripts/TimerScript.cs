using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerScript : MonoBehaviour
{
    public bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;
    public BoxCollider finishLine;

    public static TimerScript instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentTime = startMinutes * 60;
    }

    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
