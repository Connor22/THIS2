using UnityEngine;
using System.Collections;

public class PlayerCollisions: MonoBehaviour {
	
	public bool touchingDoor = false;

	private AudioSource[] sounds;
	private AudioSource gemSound;
	private AudioSource specialGemSound;
	private GameObject currentDoor;
	private PlayerChecks pCheck;
	private PlayerActions pShoot;

	void Awake(){
		sounds = GetComponents<AudioSource>();
		gemSound = sounds[3];
		specialGemSound = sounds[2];
		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();
		pShoot = GetComponent<PlayerActions>();
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Door"){
			currentDoor = coll.gameObject;
			touchingDoor = true;
		} else if (	coll.gameObject.tag == "Gem" ){
			pShoot.removeUses(-10f);
			Destroy(coll.gameObject);
			gemSound.Play ();
		} else if (coll.gameObject.tag == "JumpGem"){
			pCheck.gotJump();
			pShoot.jumpForm();
			Destroy(coll.gameObject);
			specialGemSound.Play ();
		} else if (coll.gameObject.tag == "ShotGem"){
			pCheck.gotShot();
			pShoot.shotForm();
			Destroy(coll.gameObject);
			specialGemSound.Play ();
		} else if (coll.gameObject.tag == "ShieldGem"){
			pCheck.gotShield();
			pShoot.shieldForm();
			Destroy(coll.gameObject);
			specialGemSound.Play ();
		} else if (	coll.gameObject.tag == "BigGem" ){
			pCheck.increaseMaxUses(5);
			pShoot.removeUses(-5f);
			Destroy(coll.gameObject);
			specialGemSound.Play ();
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Door"){
			currentDoor = null;
			touchingDoor = false;
		}
	}
	
	void Update(){
		if (Input.GetKey( KeyCode.Space ) && touchingDoor){
			Application.LoadLevel(currentDoor.GetComponent<DoorName>().doorName);
		}
	}
}