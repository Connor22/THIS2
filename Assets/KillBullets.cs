using UnityEngine;
using System.Collections;

public class KillBullets : MonoBehaviour {
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "EnemyBullet"){
			Destroy (coll.gameObject);
		}
	}
}
