using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health = 1;
	public GameObject gem;

	private int hp;
	private PlayerChecks pCheck;
	private Vector3 spawnPosition;
	private Quaternion spawnRotation;

	// Use this for initialization
	void Awake () {
		hp = health;
		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();
		spawnPosition = transform.position;
		spawnRotation = transform.rotation;
	}

	void playSound(){
		pCheck.playEnemyHit ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "NormalBullet"){
			playSound();
			ForwardShot bullet = coll.gameObject.GetComponent<ForwardShot>();
			if (bullet != null){
				bullet.DestroyBullet();
			}
			Debug.Log("Before: " + hp);
			if (hp > 1){
				hp--;
				Debug.Log("After: " + hp);
			} else {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "NormalBullet"){
			playSound();
			ForwardShot bullet = coll.gameObject.GetComponent<ForwardShot>();
			if (bullet != null){
				bullet.DestroyBullet();
			}
			Debug.Log("Before: " + hp);
			if (hp > 1){
				hp--;
				Debug.Log("After: " + hp);
			} else {
				Instantiate(gem, spawnPosition, spawnRotation);
				Destroy(gameObject);
			}
		}
	}
}
