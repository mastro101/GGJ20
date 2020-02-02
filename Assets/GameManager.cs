using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float Money;
    public CoinUI coinUI;
    public SoundManager soundManager;
    public CustomersQueue CustomersQueue;

    public TimerUI timerUI;
    private bool isPlaying;

    void Start()
    {
        timerUI.SetTimerText(0);
        coinUI.SetMoneyAmount(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            CustomersQueue.FillCustomers();

        if(Input.GetKeyDown(KeyCode.I))
            GetNextCustomer();
    }

    public IEnumerator StartTimer(){
        float timer=0;
        while(isPlaying){
            timer += Time.deltaTime;
            int seconds = (int)(timer % 60);
            timerUI.SetTimerText(seconds);
            yield return null;
        }
    }

    public void GetNextCustomer(){
        var customer=CustomersQueue.ServeCustomer();
        var weaponInfo=customer.GetWeapon().GetComponent<WeaponInfo>();
        HitManager.SharedInstance.SetWeapon(weaponInfo);
        weaponInfo.ResetEvents();
        weaponInfo.RegisterToOnRepairedEvent(()=>{
            Money+=weaponInfo.Value;
            coinUI.SetMoneyAmount(Money);
            StartCoroutine(RemoveLastAndGetNewCustomer());
        });

        isPlaying=true;
        StartCoroutine(StartTimer());
    }


    private IEnumerator RemoveLastAndGetNewCustomer(){
        isPlaying=false;
        CustomersQueue.RemoveLast();
        yield return new WaitForSeconds(1);
        GetNextCustomer();
    }
}