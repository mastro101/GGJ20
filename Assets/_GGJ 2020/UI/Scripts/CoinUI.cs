using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        HitManager.SharedInstance.OnCompletWeapon += SetTimerText;
    }

    void SetTimerText(WeaponInfo _weapon)
    {
        coinText.text += _weapon.EarningsCoin;
    }
}