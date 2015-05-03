using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health = 1;

	private int hp;
	private PlayerChecks pCheck;

	// Use this for initialization
	void Awake () {
		hp = health;
		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void playSound(){
		pCheck.playEnemyHit ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "NormalBullet"){
			Destroy(coll.gameObject);
			playSound();
			if (health > 1){
				hp--;
			} else {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "NormalBullet"){
			playSound();
			Destroy(coll.gameObject);
			if (health > 1){
				hp--;
			} else {
				Destroy(gameObject);
			}
		}
	}
}
