using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    int min, sec;

    public void SetTimerText(int time)
    {
        min = time / 60;
        sec = time % 60;
        timerText.text = min.ToString().ToString().PadLeft(2,'0') + " " + sec.ToString().PadLeft(2,'0');
    }
}