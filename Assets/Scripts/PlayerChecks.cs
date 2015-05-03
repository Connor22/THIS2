using UnityEngine;
using System.Collections;

public class PlayerChecks : MonoBehaviour {
	
	public bool hasJump;
	public bool hasShield;
	public bool hasShot;

	private AudioSource levelMusic;

	void Awake(){
		levelMusic = GetComponent<AudioSource>();

		hasShot = (PlayerPrefs.GetInt("hasShot") == 1);
		hasShield = (PlayerPrefs.GetInt("hasShield") == 1);
		hasJump = (PlayerPrefs.GetInt("hasJump") == 1);
	}

	public void gotJump(){
		PlayerPrefs.SetInt("hasJump", 1);
		hasJump = true;
	}
	
	public void gotShot(){
		PlayerPrefs.SetInt("hasShot", 1);
		hasShot = true;
	}
	
	public void gotShield(){
		PlayerPrefs.SetInt("hasShield", 1);
		hasShield = true;
	}
	
	public void quitProcess(){
		PlayerPrefs.SetInt("hasShield", 0);
		PlayerPrefs.SetInt("hasShot", 0);
		PlayerPrefs.SetInt("hasJump", 0);
		hasJump = false;
		hasShot = false;
		hasShield = false;
	}
	
}
