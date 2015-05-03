using UnityEngine;
using System.Collections;

public class DoorsBehaviour : MonoBehaviour {
	
	public bool touchingDoor = false;

	private GameObject currentDoor;
	private PlayerChecks pCheck;
	private PlayerShoot pShoot;

	void Awake(){
		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();
		pShoot = GetComponent<PlayerShoot>();
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Door"){
			currentDoor = coll.gameObject;
			touchingDoor = true;
		} else if (	coll.gameObject.tag == "Gem" ){
			GameObject.Find("Player").GetComponent<PlayerShoot>().uses_left += 10;
			Destroy(coll.gameObject);
		} else if (coll.gameObject.tag == "JumpGem"){
			pCheck.gotJump();
			pShoot.jumpForm();
			Destroy(coll.gameObject);
		} else if (coll.gameObject.tag == "ShotGem"){
			pCheck.gotShot();
			pShoot.shotForm();
			Destroy(coll.gameObject);
		} else if (coll.gameObject.tag == "ShieldGem"){
			pCheck.gotShield();
			pShoot.shieldForm();
			Destroy(coll.gameObject);
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