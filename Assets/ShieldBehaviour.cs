using UnityEngine;
using System.Collections;

public class ShieldBehaviour : MonoBehaviour {

	private PlayerShoot manager;

	// Use this for initialization
	void Awake () {
		manager = transform.parent.GetComponent<PlayerShoot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.layer != 12){
			gameObject.layer = 12;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "EnemyBullet" || coll.transform.tag == "Enemy"){
			manager.ShieldCounter();
		}
	}
}
