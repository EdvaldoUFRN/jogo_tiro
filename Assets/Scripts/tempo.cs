using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tempo : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeRemaing = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    void Start()
    {
    timeIsRunning = true;        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaing >= 0)
            {
                timeRemaing += Time.deltaTime;
                DisplayTime(timeRemaing);
            }
        }
    }
    void DisplayTime(float timeToDisplay){
        timeToDisplay +=1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
    }
}
