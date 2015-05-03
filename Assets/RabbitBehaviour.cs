using UnityEngine;
using System.Collections;

public class RabbitBehaviour : MonoBehaviour {

	public float speed;
	public float gravity;
	public GameObject leftBound;
	public GameObject rightBound;
	public bool startRight = true;

	private bool facingRight;
	private float movingSpeed;
	//private Vector3 currPos;
	//private Vector3 prevPos;

	// Use this for initialization
	void Awake () {
		facingRight = startRight;
		movingSpeed = speed;
		if (!startRight){
			movingSpeed = -1 * movingSpeed;
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void Start(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, -gravity);
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
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
}
