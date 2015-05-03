using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health = 1;

	private int hp;

	// Use this for initialization
	void Start () {
		hp = health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "Enemy"){
			if (health > 1){
				hp--;
			} else {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "Enemy"){
			if (health > 1){
				hp--;
			} else {
				Destroy(gameObject);
			}
		}
	}
}
