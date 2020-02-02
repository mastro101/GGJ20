using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float Money;
    public CoinUI coinUI;
    public SoundManager soundManager;
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
            Money+=weaponInfo.Value;
            coinUI.SetMoneyAmount(Money);
        });

    }


    public float GetMoney(){
        return Money;
    }
}