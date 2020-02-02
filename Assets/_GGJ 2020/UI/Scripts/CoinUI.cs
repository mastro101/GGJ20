using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    public void SetMoneyAmount(float value)
    {
        coinText.text = value.ToString();
    }
}