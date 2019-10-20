﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopWatch : MonoBehaviour
{
    public float timeStart = 0f;

    public int seconds;
    public int minutes;
    public float GoodTime;
    public bool timerActive = false;

    public float GazeTime = 0;
    
    public GameObject MissionText;
    public GameObject GoodText;
    public GameObject Mark;
    public GameObject Cam;
    public GameObject Badmark;

    void Update()
    {
        if (timerActive)
        {
            timeStart += Time.deltaTime;

            seconds = (int)(timeStart % 60);
            minutes = (int)(timeStart / 60) % 60;

            if (timeStart > 0)
            {
                string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

                this.GetComponent<TextMeshPro>().text = timerString;
            }
        }


        if (seconds >= 8 && GazeTime <= 5.7f)
        {
            MissionText.SetActive(true);
            Mark.SetActive(true);
            GazeTime += Time.deltaTime;
        }
        else MissionText.SetActive(false);

        if (GazeTime > 5.7f && GoodTime <= 3)
        {
            MissionText.SetActive(false);
            GoodText.SetActive(true);
            Mark.SetActive(false);
            GoodTime += Time.deltaTime;
        }
        else GoodText.SetActive(false);

        if (Cam.transform.rotation.x > -53 && Cam.transform.rotation.x < 53)
        {
            Badmark.SetActive(false);
        }
        else Badmark.SetActive(true);
    }

}
