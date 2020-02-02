using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
	
	public GameObject pauseMenu;
	bool pause = false;
	public GameObject winMenu, loseMenu;
	
	private SoundManager soundManager;
	
    void Start()
    {       
		PauseGame(false);	
		winMenu.SetActive(false);
		loseMenu.SetActive(false);
		
		soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Q)) {
		 	pause = !pause;
		 	PauseGame(pause);
		 }
		
		//if(Input.GetKeyDown(KeyCode.A)) {
		//	WinGame();
		//}
		
		//if(Input.GetKeyDown(KeyCode.S)) {
		//	LoseGame();
		//}
    }
	
	public void PauseGame(bool newState) {
		pause = newState;
		if(pause) {
			Time.timeScale = 0f;
			pauseMenu.SetActive(true);
		}
		else {
			Time.timeScale = 1f;
			pauseMenu.SetActive(false);
		}
	}
	
	public void WinGame() {
		Time.timeScale = 0f;
		winMenu.SetActive(true);
		soundManager.Play("Vittoria2");
	}
	
	public void LoseGame() {
		Time.timeScale=0f;
		loseMenu.SetActive(true);
		soundManager.Play("Sconfitta");
	}
}
