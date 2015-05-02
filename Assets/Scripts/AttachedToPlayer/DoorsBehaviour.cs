using UnityEngine;
using System.Collections;

public class DoorsBehaviour : MonoBehaviour {
	
	public bool touchingDoor = false;

	private GameObject currentDoor;
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Door"){
			currentDoor = coll.gameObject;
			touchingDoor = true;
		}
		Debug.Log("Collision with: " + coll.gameObject.tag);
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Door"){
			currentDoor = null;
			touchingDoor = false;
		}
		Debug.Log("Stopped collision with: " + coll.gameObject.tag);
	}
	
	void Update(){
		if (Input.GetKey( KeyCode.Space ) && touchingDoor){
			Application.LoadLevel(currentDoor.GetComponent<DoorName>().doorName);
		}
	}
}