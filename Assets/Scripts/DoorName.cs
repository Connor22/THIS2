using UnityEngine;
using System.Collections;

public class DoorName : MonoBehaviour {
	public string doorName;

	public void Awake(){
		if (!(doorName == "MapLevel") && PlayerPrefs.GetInt("beat" + doorName) == 1){
			Destroy(gameObject);
		}
	}
}
