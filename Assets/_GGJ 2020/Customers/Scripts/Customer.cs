using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject customerIcon;
    public Vector3 iconOffset;
    public int difficulty;

    private GameObject weapon;
    void Awake(){
        weapon = Instantiate(weaponPrefab, new Vector3(0,0,0), Quaternion.identity);
        weapon.transform.parent=gameObject.transform;
    }

    public GameObject GetWeapon(){
        return weapon;
    }
    
    public void SetCustomerIconPosition(Transform transform){
        customerIcon.transform.parent=transform;
        customerIcon.transform.position=transform.position+iconOffset;
    }

    public int GetDifficulty(){
        return difficulty;
    }

    public void ActivateWithoutSword(){
        gameObject.SetActive(true);
        weapon.SetActive(false);
    } 

    public void Activate(Vector2 spawnPosition){
        gameObject.SetActive(true);
        weapon.SetActive(true);
        PlaceWeaponInWorld(spawnPosition);
    }

    public void Deactivate(){
        gameObject.SetActive(false);
        weapon.SetActive(false);
    }

    private void PlaceWeaponInWorld(Vector2 spawnPosition)
    {
        var weaponInfo=weapon.GetComponent<WeaponInfo>();
        weapon.transform.position=spawnPosition+weaponInfo.offset;
    }
}