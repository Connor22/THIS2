using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int health = 1;
	public int continues = 1;

	private int hp;
	private int lives;
	private Vector3 start;
	private PlayerActions power_up;
	private AudioSource[] Sounds;
	private AudioSource hitSound;

	// Use this for initialization
	void Start () {
		hp = health;
		lives = continues;
		start = transform.position;
		power_up = gameObject.GetComponent<PlayerActions>();

		Sounds = GetComponents<AudioSource>();
		hitSound = Sounds[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnCollisionEnter2D(Collision2D coll){
		//Debug.Log("Layer: " + gameObject.layer);
		if (coll.transform.tag == "Enemy" || coll.transform.tag == "EnemyBullet"){
			if (health > 1){
				hp--;
			} else {
				LoseLife();
			}
		}
		if (coll.transform.tag == "Death"){
			LoseLife();
		}
		Debug.Log("Collision with: " + coll.gameObject.tag);
	}*/

	public void LoseLife(){
		//if (lives > 0){
			transform.position = start;
			//lives--;
			hp = health;
		//}

		hitSound.Play();
	}
}
