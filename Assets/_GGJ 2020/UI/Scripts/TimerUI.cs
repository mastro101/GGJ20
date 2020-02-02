using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    int min, sec;

    void SetTimerText(int _time)
    {
        min = _time / 60;
        sec = _time % 60;
        timerText.text = "0" + min.ToString() + " " + sec.ToString();
    }
}