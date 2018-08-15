using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instace;

	public AudioSFX audioSFX;
	public AudioPlayer audioPlayer;


void Awake(){
	if(instace == null){
		instace = this;
	}
}
	public void PlayCoinPickup(GameObject obj){
		AudioSource.PlayClipAtPoint(audioSFX.CoinPickUp, obj.transform.position);
	}
	public void PlayJumpSound(GameObject obj){
		AudioSource.PlayClipAtPoint(audioPlayer.Jump, obj.transform.position);
	}
	public void PlayFailSound(GameObject obj){
		AudioSource.PlayClipAtPoint(audioSFX.Fail, obj.transform.position);
	}
}

