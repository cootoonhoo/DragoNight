﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {
	public static GM instance = null;

	public float yMinLive = -5f;

	PlayerController player;
	public Transform spawnPoint;
	public GameObject playerPrefab;
	public float timeToRespwan = 1.5f;
	public float maxTime = 120f;
	bool timerOn = true;
	float timeLeft;
	public UI ui;
	GameData data = new GameData();

	void Awake(){
			if (instance == null){
				instance = this;
			}
			else if(instance !=this){
				Destroy(gameObject);
			}
			DontDestroyOnLoad(gameObject);

	}

	// Use this for initialization
	void Start () {
		timeLeft= maxTime;
		if (player == null){
			RespawnPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null){
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if(obj != null){
			player = obj.GetComponent<PlayerController>();
			}
		}
		UpdateTimer();
		DisplayHudData();
		
	}
	public void RestartLevel(){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void ExitToMainMenu(){
				LoadScene("MainMenu");
		}

		public void LoadScene(string sceneName){
			SceneManager.LoadScene(sceneName);
		}
		public void CloseApp(){
			Application.Quit();
		}
		void UpdateTimer(){
			if(timerOn){
				timeLeft= timeLeft - Time.deltaTime;
				if(timeLeft <= 0f){
					timeLeft = 0;
					ExpirePlayer();
					GameOver();
				}
			}
		}
	 void DisplayHudData(){
		 	ui.hud.txtCoinCount.text = "x " + data.coinCount;
			 ui.hud.txtLifeCount.text = "x " + data.lifeCount;
	 		ui.hud.txtTimer.text = "Time " + timeLeft.ToString("F1");
			}

	public void IncrementCoinCount(){
		data.coinCount ++;
	}
	public void DecrementLives(){
		data.lifeCount--;
	}
	public void RespawnPlayer () {
		if(timeLeft > 0f){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		}
	}

	public void KillPlayer(){
		if(player!= null){
			Destroy(player.gameObject);
			DecrementLives();
			if(data.lifeCount > 0 ){
			Invoke("RespawnPlayer", timeToRespwan);
			}
			else{
				GameOver();
			}
		}		
	}
	public void ExpirePlayer(){
		if(player!= null){
			Destroy(player.gameObject);
		}
		GameOver();
	}
	void GameOver(){
		timerOn = false;
		ui.gameOver.txtCoinCount.text = "Coins " + data.coinCount;
		ui.gameOver.txtTimer.text = "Timer: "+ timeLeft.ToString("F1");
		ui.gameOver.gameOverPanel.SetActive(true);
	}
}
