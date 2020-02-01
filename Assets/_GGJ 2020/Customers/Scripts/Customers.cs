using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    [SerializeField] SwordMovement swordPrefab;
    public Sprite customerIcon;
    // Temporaneamente pubblico finchè non sarà calcolata dai valori della spada
    public int difficult;
    //
    [HideInInspector] public SwordMovement currentSword;

    public SwordMovement InstantiateSword(Vector2 spawnPosition)
    {
        currentSword = Instantiate(swordPrefab, spawnPosition, Quaternion.identity).GetComponent<SwordMovement>();
        return currentSword;
    }
}