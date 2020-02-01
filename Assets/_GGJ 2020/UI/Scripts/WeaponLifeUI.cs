using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLifeUI : MonoBehaviour
{
    [SerializeField] Image WeaponLifeImage;

    private void Start()
    {
        WeaponLifeImage.fillAmount = 0;
        HitManager.SharedInstance.OnCorrectHit += FIllAddLifeImage;
    }

    public void FillTotalLifeImage(WeaponInfo _weapon)
    {
        int delta = 1 / _weapon.maxLife;
        WeaponLifeImage.fillAmount += delta;
    }

    public void FIllAddLifeImage(WeaponInfo _weapon)
    {
        int delta = 1 / _weapon.maxLife;
        WeaponLifeImage.fillAmount = delta * _weapon.currentLife;
    }
}