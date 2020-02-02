using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] WeaponInfo weaponPrefab;
    public Sprite customerIcon;
    // Temporaneamente pubblico finchè non sarà calcolata dai valori della spada
    public int difficult;
    //
    [HideInInspector] public WeaponInfo currentSword;

    public WeaponInfo InstantiateSword(Vector2 spawnPosition)
    {
        var weaponInfo=weaponPrefab.GetComponent<WeaponInfo>();
        currentSword = Instantiate(weaponPrefab, spawnPosition+weaponInfo.offset, Quaternion.identity);
        return currentSword;
    }
}