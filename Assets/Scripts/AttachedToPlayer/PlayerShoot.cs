using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public enum Its { Shot, Jump, Shield };
	// S.H.O.T.: Shell Hits Opposing Targets
	// J.U.M.P.: Just Up More Please
	// S.H.I.E.L.D.: Set-up Helpful Impenetrable Energy Layer of Defence

	public GameObject ammo;
	public float delay;
	public int max_shot = 1;
	public Its state = Its.Shot;

	public float total_uses = 100;
	public float shot_cost;
	public float jump_cost;
	public float shield_cost;
	public float uses_left;


	private float timer;

	// Use this for initialization
	void Awake () {
		timer = 0;
		state = Its.Shot;
		uses_left = total_uses;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1)){
			state = Its.Shot;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			state = Its.Jump;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			state = Its.Shield;
		}

		if (state = Its.Shot && (uses_left - shot_cost) > 0){
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else if (Input.GetAxis("Fire") > 0 && ForwardShot.bullet_count < max_shot) {
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
				uses_left -= shot_cost;
			}
		} else if (state = Its.Shield && (uses_left - shield_cost) > 0) {
		}

	}

	public void JumpCounter(){
	}

	public void ShieldCounter(){
	}
}
