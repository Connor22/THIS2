using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public GameObject ammo;
	public float delay;
	public int max_shot = 1;

	private float timer;

	// Use this for initialization
	void Awake () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else if (Input.GetAxis("Fire") > 0 && ForwardShot.bullet_count < max_shot){
			float directionFaced = transform.localScale.x / Mathf.Abs(transform.localScale.x);
			Vector3 tempVec = transform.position;
			if (directionFaced > 0){
				tempVec.x = tempVec.x + 0.5f;
			} else {
				tempVec.x = tempVec.x - 0.5f;
			}
			GameObject ammoInstance = (GameObject)Instantiate(ammo, tempVec, transform.rotation);
			ammoInstance.GetComponent<ForwardShot>().SendMessage("setDirection", directionFaced);
			timer = delay;
		}
	}
}
