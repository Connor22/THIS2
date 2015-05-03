using UnityEngine;
using System.Collections;

public class RabbitBehaviour : MonoBehaviour {

	public float speed;
	public float gravity;
	public GameObject leftBound;
	public GameObject rightBound;
	public bool startRight = true;

	private bool facingRight;
	//private float gravity;
	private float movingSpeed;
	//private Vector3 currPos;
	//private Vector3 prevPos;

	// Use this for initialization
	void Awake () {
		//gravity = GetComponent<Rigidbody2D>().gravityScale;
		facingRight = startRight;
		movingSpeed = speed;
		if (!startRight){
			movingSpeed = -1 * movingSpeed;
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void Start(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, -gravity);
		//GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, 0f);
		//currPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x <= leftBound.transform.position.x && !facingRight) || (transform.position.x >= rightBound.transform.position.x && facingRight))
			ChangeDirection();
		/*prevPos = currPos;
		currPos = transform.position;
		if (prevPos.x == currPos.x)
			ChangeDirection();*/
	}

	void ChangeDirection(){
		facingRight = !facingRight;
		movingSpeed = -1 * movingSpeed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, -gravity);
		//GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, 0f); 
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "Platform"){
			if((coll.transform.position.x < transform.position.x && !facingRight) || (coll.transform.position.x > transform.position.x && facingRight))
				ChangeDirection();
			//if (coll.transform.position.y < transform.position.y)
				//GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "Platform"){
			if((coll.transform.position.x < transform.position.x && !facingRight) || (coll.transform.position.x > transform.position.x && facingRight))
				ChangeDirection();
			//if (coll.transform.position.y < transform.position.y)
				//GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}

	/*void OnTriggerExit2D(Collider2D coll){
		if (coll.transform.tag == "Platform" && coll.transform.position.y < transform.position.y){
			GetComponent<Rigidbody2D>().gravityScale = gravity;
		}
	}

	void OnCollisiomExit2D(Collision2D coll){
		if (coll.transform.tag == "Platform" && coll.transform.position.y < transform.position.y){
			GetComponent<Rigidbody2D>().gravityScale = gravity;
		}
	}*/
}
