using UnityEngine;
using System.Collections;

public class ForwardShot : MonoBehaviour {

	public float speed;
	public static int bullet_count = 0;
	

	// Use this for initialization
	void Awake () {
		if (gameObject.tag == "NormalBullet")
			bullet_count++;
	}

	void OnBecameInvisible(){
		DestroyBullet();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void setDirection(int direction){
		float tempSpeed = speed * (float)direction;
		GetComponent<Rigidbody2D>().velocity = new Vector2(tempSpeed, 0f); 
	}

	public void DestroyBullet(){
		if (gameObject.tag == "NormalBullet")
			bullet_count--;
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platform")
			DestroyBullet();
	}

	/** Look at Sean's GMD for bullet collision handling */

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Platform")
			DestroyBullet();
	}
}
