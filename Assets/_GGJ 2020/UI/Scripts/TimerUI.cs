using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    void SetTimerText(int _time)
    {
        timerText.text = _time.ToString();
    }
}