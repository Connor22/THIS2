using UnityEngine;
using System.Collections;

public class GoatBehaviour : MonoBehaviour {

	public float gravity;
	public float jump;

	private float fallSpeed;
	private float jumpHeight;
	private Rigidbody2D goat;

	// Use this for initialization
	void Start () {
		fallSpeed = gravity;
		jumpHeight = jump;
		goat = gameObject.GetComponent<Rigidbody2D>();
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "Platform" && coll.transform.position.y < transform.position.y){
			GoatJump();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "Platform" && coll.transform.position.y < transform.position.y){
			GoatJump();
		}
	}

	void GoatJump(){
		goat.AddForce(new Vector2(0f, jump));
	}

}
