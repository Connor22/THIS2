using UnityEngine;
using System.Collections;

public class PlayerCollisions: MonoBehaviour {

	private AudioSource[] sounds;
	private AudioSource gemSound;
	private AudioSource specialGemSound;
	private AudioSource hitSound;
	private PlayerChecks pCheck;
	private PlayerActions pShoot;

	private bool touchingDoor = false;
	private GameObject currentDoor;

	private bool isTouchingGem = false;
	private GameObject currentGem;

	private bool isTouchingBigGem = false;
	private GameObject currentBigGem;

	void Awake(){
		sounds = GetComponents<AudioSource>();
		hitSound = sounds[4];
		gemSound = sounds[3];
		specialGemSound = sounds[2];

		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();
		pShoot = GetComponent<PlayerActions>();
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		switch (coll.gameObject.tag)
		{
			case "Door":
				currentDoor = coll.gameObject;
				touchingDoor = true;
				break;
			case "Gem":
				isTouchingGem = true;
				currentGem = coll.gameObject;
				break;
			case "JumpGem":
				pCheck.gotJump();
				pShoot.jumpForm();
				Destroy(coll.gameObject);
				specialGemSound.Play ();
				break;
			case "ShotGem":
				pCheck.gotShot();
				pShoot.shotForm();
				Destroy(coll.gameObject);
				specialGemSound.Play ();
				break;
			case "ShieldGem":
				pCheck.gotShield();
				pShoot.shieldForm();
				Destroy(coll.gameObject);
				specialGemSound.Play ();
				break;
			case "BigGem":
				currentBigGem = coll.gameObject;
				isTouchingBigGem = true;
				break;
			case "Enemy":
			case "EnemyBullet":
				if (!(gameObject.tag == "Shield")){
					LoseLife();
				}	
				break;
			case "Death":
				LoseLife();
				break;
			default:
				Debug.Log(coll.gameObject.tag);
				break;
		}
	}

	
	void OnTriggerExit2D(Collider2D coll){
		switch (coll.gameObject.tag)
		{
		case "Door":
			currentDoor = null;
			touchingDoor = false;
			break;
		case "Gem":
			currentGem = null;
			isTouchingGem = false;
			break;
		case "BigGem":
			currentBigGem = null;
			isTouchingBigGem = false;
			break;
		default:
			break;
		}
	}

	void LoseLife(){
		pCheck.revertProcess();
		hitSound.Play();
		StartCoroutine(wait(2));
		Application.LoadLevel(Application.loadedLevelName);
	}

	IEnumerator wait(int seconds){
		yield return new WaitForSeconds(seconds);
	}

	void Update(){
		if (Input.GetKey( KeyCode.Space ) && touchingDoor){
			Application.LoadLevel(currentDoor.GetComponent<DoorName>().doorName);
			PlayerPrefs.SetInt("beat" + Application.loadedLevelName, 1);
		}
		if (isTouchingBigGem && !(gameObject.tag == "Shield")){
			pCheck.increaseMaxUses(5);
			pShoot.removeUses(-5f);
			Destroy(currentBigGem);
			specialGemSound.Play ();
			isTouchingBigGem = false;
		}
		if (isTouchingGem && !(gameObject.tag == "Shield")){
			pShoot.removeUses(-10f);
			Destroy(currentGem);
			gemSound.Play ();
			isTouchingGem = false;
		}
	}
}