using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
	
	public GameObject pauseMenu;
	public bool pause = false;
	
    void Start()
    {
        pauseMenu=GameObject.Find("Pause Menu");
		PauseGame(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
			pause = !pause;
			PauseGame(pause);
		}
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
}
