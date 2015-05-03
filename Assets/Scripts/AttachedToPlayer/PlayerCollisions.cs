using UnityEngine;
using System.Collections;

public class PlayerCollisions: MonoBehaviour {
	
	public bool touchingDoor = false;

	private AudioSource[] sounds;
	private AudioSource gemSound;
	private AudioSource specialGemSound;
	private AudioSource hitSound;
	private GameObject currentDoor;
	private PlayerChecks pCheck;
	private PlayerActions pShoot;

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
				pShoot.removeUses(-10f);
				Destroy(coll.gameObject);
				gemSound.Play ();
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
				pCheck.increaseMaxUses(5);
				pShoot.removeUses(-5f);
				Destroy(coll.gameObject);
				specialGemSound.Play ();
				break;
			case "Enemy":
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
		if (coll.gameObject.tag == "Door"){
			currentDoor = null;
			touchingDoor = false;
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
		}
	}
}