using UnityEngine;
using System.Collections;

public class PlayerChecks : MonoBehaviour {
	
	public bool hasJump;
	public bool hasShield;
	public bool hasShot;

	public int currentMaxUses;

	private AudioSource[] sounds;
	private AudioSource levelMusic;

	[HideInInspector]
	private AudioSource enemyHit;
	private AudioSource enemyShoot;

	void Awake(){
		sounds = GetComponents<AudioSource>();

		levelMusic = sounds[0];

		enemyHit = sounds[1];
		enemyShoot = sounds[2];

		currentMaxUses = PlayerPrefs.GetInt("maxUses");

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

	public void increaseMaxUses(int num){
		currentMaxUses += num;
		PlayerPrefs.SetInt("maxUses", currentMaxUses);
	}

	public void playEnemyHit(){
		enemyHit.Play();
	}

	public void playEnemyShoot(){
		enemyShoot.Play();
	}
	
}
