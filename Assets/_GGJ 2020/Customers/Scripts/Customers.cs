using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public SwordMovement swordPrefab;
    public Sprite customerIcon;
    public int difficult;

    public SwordMovement InstantiateSword(Vector2 spawnPosition)
    {
        return Instantiate(swordPrefab, spawnPosition, Quaternion.identity).GetComponent<SwordMovement>();
    }
}