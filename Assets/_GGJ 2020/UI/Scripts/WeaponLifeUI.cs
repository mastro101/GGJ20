using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLifeUI : MonoBehaviour
{
    [SerializeField] Image WeaponLifeImage;

    public void Reset(){
        WeaponLifeImage.fillAmount = 0;
    }

    public void SetFillAmount(float value)
    {
        WeaponLifeImage.fillAmount = 1-value;
    }
}