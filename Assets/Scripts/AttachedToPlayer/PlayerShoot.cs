using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public enum Its { Shot, Jump, Shield };
	// S.H.O.T.: Shell Hits Opposing Targets
	// J.U.M.P.: Just Up More Please
	// S.H.I.E.L.D.: Set-up Helpful Impenetrable Energy Layer of Defence

	public GameObject ammo;
	public GameObject shield;
	public float delay;
	public int max_shot = 1;
	public Its state = Its.Shot;

	public float total_uses = 100;
	public float shot_cost;
	public float jump_cost;
	public float shield_cost;
	public float uses_left;


	private float timer;
	//private GameObject shield_instance = null;

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
			DestroyShield();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			state = Its.Jump;
			timer = 0;
			DestroyShield();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			state = Its.Shield;
			timer = 0;
		}

		if (state == Its.Shot && (uses_left - shot_cost) > 0){
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
		} else if (state == Its.Shield && (uses_left - shield_cost) > 0) {
			if (Input.GetKeyDown(KeyCode.Space) && !shield.activeSelf){
				//shield_instance = (GameObject)Instantiate(shield, transform.position, transform.rotation);
				//shield_instance.transform.parent = transform;
				shield.SetActive(true);
				gameObject.tag = "Shield";
				//gameObject.layer = 13;
				//shield_instance.SendMessage("SetManager", gameObject);
			}
			if (Input.GetKeyUp(KeyCode.Space)){
				//gameObject.layer = 12;
				DestroyShield();
				gameObject.tag = "Player";
			}
		}

	}

	void DestroyShield(){
		if (shield != null){
			shield.SetActive(false);
		}
	}

	public void JumpCounter(){
		uses_left -= jump_cost;
		print ("Jump - Uses left: " + uses_left);
	}

	public void ShieldCounter(){
		uses_left -= shield_cost;
		print ("Shield - Uses left: " + uses_left);
		if ((uses_left - shield_cost) > 0){
			DestroyShield();
		}
	}
}
