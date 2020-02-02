using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	
	public Sprite[] epicPhraseFrames;
	public GameObject epicPhrasePrefab;
	public float epicPhraseSpeed = 0.1f;

    void Start()
    {
        CustomersQueue.onQueueChange += CheckLoseCondiction;
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
        {
            StartGame();
			StartCoroutine("PlayEpicPhraseAnimation");
        }
    }

    private void StartGame()
    {
        isPlaying = true;
        StartCoroutine(StartTimer());
        GetNextCustomer();
    }

    public IEnumerator StartTimer(){
        float timer=durationOfGameInSecond;
        while(timer > 0){
            timer -= Time.deltaTime;
            int seconds = (int)Mathf.Floor(timer);
            timerUI.SetTimerText(seconds);
            currentTime = timer;
            if (currentTime < 0)
                isPlaying = false;
            yield return null;
        }
        EndGame(true);
    }

    public void GetNextCustomer(){
		
		StartCoroutine("PlayEpicPhraseAnimation");
		
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
    }


    private IEnumerator RemoveLastAndGetNewCustomer(){
        isPlaying=false;
        CustomersQueue.RemoveLast();
        ResetUI();
        yield return new WaitForSeconds(1);
        weaponLifeUI.Reset();
        GetNextCustomer();
    }

    void CheckLoseCondiction(int _customerInQueue)
    {
        if (_customerInQueue == CustomersQueue.maxCustomers)
            EndGame(false);
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
	
	private IEnumerator PlayEpicPhraseAnimation() {
		GameObject epicPhraseImage = Instantiate(epicPhrasePrefab, new Vector3(180,100,0), transform.rotation);
		for(int i=0; i<epicPhraseFrames.Length; i++) {
			yield return new WaitForSeconds(epicPhraseSpeed);
			epicPhraseImage.GetComponent<SpriteRenderer>().sprite = epicPhraseFrames[i];
		}
		yield return new WaitForSeconds(epicPhraseSpeed);
		Destroy(epicPhraseImage.gameObject);
	}
	
}