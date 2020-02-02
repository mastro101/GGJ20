using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float Money;

    public CustomersQueue CustomersQueue;

    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            CustomersQueue.FillCustomers();

        if(Input.GetKeyDown(KeyCode.I))
            CustomersQueue.ServeCustomer();
    }

    public void GetNextCustomer(){
        var customer=CustomersQueue.ServeCustomer();
        var weaponInfo=customer.GetWeapon().GetComponent<WeaponInfo>();
        HitManager.SharedInstance.SetWeapon(weaponInfo);
        weaponInfo.ResetEvents();
        weaponInfo.RegisterToOnRepairedEvent(()=>{
            Money
        });

    }

    private void OnRepaired(){
         HitManager.SharedInstance.
    }

    public float GetMoney(){
        return Money;
    }
}