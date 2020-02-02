using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int durationOfGameInSecond;
    
    private float Money;
    public CoinUI coinUI;
    public WeaponLifeUI weaponLifeUI;
    public PauseSystem pauseSystem;

    public SoundManager soundManager;
    public CustomersQueue CustomersQueue;
    
    [HideInInspector] public float currentTime;

    public TimerUI timerUI;
    private bool isPlaying;

    void Start()
    {
        ResetUI();
    }
    
    private void ResetUI(){
        timerUI.SetTimerText(0);
        coinUI.SetMoneyAmount(Money);
        weaponLifeUI.Reset();
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
        float timer=durationOfGameInSecond;
        while(isPlaying){
            timer -= Time.deltaTime;
            int seconds = (int)(timer);
            timerUI.SetTimerText(seconds);
            currentTime = timer;
            if (currentTime < 0)
                isPlaying = false;
            yield return null;
        }
        EndGame(true);
    }

    public void GetNextCustomer(){
        var customer=CustomersQueue.ServeCustomer();
        var weaponInfo=customer.GetWeapon().GetComponent<WeaponInfo>();
        HitManager.SharedInstance.SetWeapon(weaponInfo);
        weaponInfo.Reset();
        weaponInfo.RegisterToOnRepairedEvent(()=>{
            Money+=weaponInfo.Value;
            coinUI.SetMoneyAmount(Money);
            StartCoroutine(RemoveLastAndGetNewCustomer());
            isPlaying=false;
        });

        StartCoroutine(StartTimer());
    }


    private IEnumerator RemoveLastAndGetNewCustomer(){
        isPlaying=false;
        CustomersQueue.RemoveLast();
        ResetUI();
        yield return new WaitForSeconds(1);
        weaponLifeUI.Reset();
        GetNextCustomer();
    }

    public void EndGame(bool _isWin)
    {
        if (_isWin)
        {
            pauseSystem.WinGame();
        }
        else
        {
            pauseSystem.LoseGame();
        }
    }
}