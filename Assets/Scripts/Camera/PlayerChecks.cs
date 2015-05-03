using UnityEngine;
using System.Collections;

public class PlayerChecks : MonoBehaviour {
	
	public bool hasJump;
	public bool hasShield;
	public bool hasShot;

	[HideInInspector]
	public int currentMaxUses;
	public int uses_left = 999;

	private AudioSource[] sounds;
	private AudioSource levelMusic;

	[HideInInspector]
	private AudioSource enemyHit;
	private AudioSource enemyShoot;
	private PlayerActions pActions;

	private int oldMaxUses;
	private int hasShotOrig;
	private int hasShieldOrig;
	private int hasJumpOrig;

	void Awake(){

		if (PlayerPrefs.GetInt("justLoaded") == 1)
			quitProcess();

		sounds = GetComponents<AudioSource>();
		pActions = GameObject.Find("Player").GetComponent<PlayerActions>();

		levelMusic = sounds[0];

		enemyHit = sounds[1];
		enemyShoot = sounds[2];

		oldMaxUses = PlayerPrefs.GetInt("maxUses");
		currentMaxUses = oldMaxUses;
		uses_left = currentMaxUses;

		hasShotOrig = (int)PlayerPrefs.GetInt("hasShot");
		hasShieldOrig = (int)PlayerPrefs.GetInt("hasShield");
		hasJumpOrig = (int)PlayerPrefs.GetInt("hasJump");

		hasShot = (hasShotOrig == 1);
		hasShield = (hasShieldOrig == 1);
		hasJump = (hasJumpOrig == 1);
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
		PlayerPrefs.SetInt("maxUses", 10);
		PlayerPrefs.SetInt("beatMapLevel", 0);
		PlayerPrefs.SetInt("beatJumpGemRoom", 0);
		PlayerPrefs.SetInt("beatShieldGemRoom", 0);
		PlayerPrefs.SetInt("beatShotGemRoom", 0);
		PlayerPrefs.SetInt("beatMapSceneB", 0);
		hasJump = false;
		hasShot = false;
		hasShield = false;
		print("Reset");
	}

	public void revertProcess(){
		PlayerPrefs.SetInt("hasShield", hasShieldOrig);
		PlayerPrefs.SetInt("hasShot", hasShotOrig);
		PlayerPrefs.SetInt("hasJump", hasJumpOrig);
		PlayerPrefs.SetInt("maxUses", oldMaxUses);
	}

	public void increaseMaxUses(int num){
		currentMaxUses += num;
		print(currentMaxUses);
		PlayerPrefs.SetInt("maxUses", currentMaxUses);
	}

	public void playEnemyHit(){
		enemyHit.Play();
	}

	public void playEnemyShoot(){
		enemyShoot.Play();
	}

	public int getUsesLeft(){
		print (uses_left);
		return uses_left;
	}

	public void setUsesLeft(int value){
		uses_left = value;
	}

	public int getMaxUses(){
		return currentMaxUses;
	}
	
	public void setMaxUses(int value){
		currentMaxUses = value;
	}

	void Update () {
		if (Input.GetKey( KeyCode.Escape )){
			quitProcess();
			Application.Quit();
		} else if (Input.GetKey( KeyCode.R )){
			revertProcess();
			Application.LoadLevel(Application.loadedLevelName);
		} else if (Input.GetKey( KeyCode.M )){
			quitProcess();
		}
	}
}
