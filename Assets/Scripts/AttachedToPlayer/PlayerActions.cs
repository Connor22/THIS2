using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	public enum Its {Null, Shot, Jump, Shield };
	// S.H.O.T.: Shell Hits Opposing Targets
	// J.U.M.P.: Just Up More Please
	// S.H.I.E.L.D.: Set-up Helpful Impenetrable Energy Layer of Defence

	public GameObject ammo;
	public GameObject shield;

	public float delay;
	public int max_shot = 1;
	public float ammoSize;

	[HideInInspector]
	public Its state = Its.Shot;

	public float shot_cost;
	public float jump_cost;
	public float shield_cost; 
	
	public float total_uses;

	[HideInInspector]
	public float uses_left;

	private PlayerChecks pCheck;
	private float timer;
	private float shieldtimer;
	private bool isShieldActive;
	//private GameObject shield_instance = null;

	private AudioSource[] Sounds;
	private AudioSource shotSound;
	
	// Use this for initialization
	void Awake () {
		Sounds = GetComponents<AudioSource>();
		shotSound = Sounds[5];

		pCheck = GameObject.Find("Main Camera").GetComponent<PlayerChecks>();

		timer = 0;
		state = Its.Null;
		uses_left = total_uses;
		removeUses(0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1)){
			nullForm();
		} else if (Input.GetKeyDown(KeyCode.Alpha2) && pCheck.hasJump){
			jumpForm();
		} else if (Input.GetKeyDown(KeyCode.Alpha3) && pCheck.hasShield){
			shieldForm();
		} else if (Input.GetKeyDown(KeyCode.Alpha4) && pCheck.hasShot){
			shotForm();
		}

		if (state == Its.Shot && (uses_left - shot_cost) > 0){
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else if (Input.GetAxis("Fire") > 0 && ForwardShot.bullet_count < max_shot) {
				fire();
			}
		} else if (state == Its.Shield && (uses_left - shield_cost) > 0) {
			if (Input.GetKeyDown(KeyCode.Space) && !shield.activeSelf){
				shield.SetActive(true);
				isShieldActive = true;
				gameObject.tag = "Shield";
				removeUses(shield_cost);
			}
			if (Input.GetKeyUp(KeyCode.Space)){
				//gameObject.layer = 12;
				DestroyShield();
				gameObject.tag = "Player";
			}
		}

		if (isShieldActive){
			shieldtimer += Time.deltaTime;
		} else {
			shieldtimer = 0f;
		}

		if (shieldtimer > 1f){
			removeUses(shield_cost);
			print(uses_left);
			shieldtimer = 0f;
		}
	}

	public void jumpForm(){
		gameObject.tag = "Player";
		state = Its.Jump;
		timer = 0;
		DestroyShield();
		GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
	}
	
	public void shieldForm(){
		state = Its.Shield;
		timer = 0;
		GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f);
	}
	
	public void shotForm(){
		gameObject.tag = "Player";
		state = Its.Shot;
		DestroyShield();
		GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
	}

	public void nullForm(){
		state = Its.Null;
		DestroyShield();
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
	}
	
	void fire(){
		shotSound.Play();
		float directionFaced = transform.localScale.x / Mathf.Abs(transform.localScale.x);
		Vector3 tempVec = transform.position;
		if (directionFaced > 0){
			tempVec.x = tempVec.x + 0.5f;
		} else {
			tempVec.x = tempVec.x - 0.5f;
		}
		GameObject ammoInstance = (GameObject)Instantiate(ammo, tempVec, transform.rotation);
		ammoInstance.GetComponent<ForwardShot>().SendMessage("setDirection", directionFaced);
		Vector3 ammoscale = ammoInstance.transform.localScale;
		ammoscale.x = ammoscale.y = ammoSize;
		ammoInstance.transform.localScale = ammoscale;
		timer = delay;
		removeUses(shot_cost);
	}

	public void removeUses(float cost){

		uses_left -= cost;

		print (uses_left);

		if (uses_left > pCheck.currentMaxUses){
			uses_left = pCheck.currentMaxUses;
		}

		int uses_left_int = (int)uses_left;
		int hundreds = uses_left_int / 100;
		int tens = uses_left_int / 10 - hundreds * 10;
		int singles = uses_left_int - hundreds * 100 - tens * 10;
		GameObject.Find("Hundreds").GetComponent<DigitUpdater>().updateDigit(hundreds);
		GameObject.Find("Tens").GetComponent<DigitUpdater>().updateDigit(tens);
		GameObject.Find("Singles").GetComponent<DigitUpdater>().updateDigit(singles);
	}

	void DestroyShield(){
		if (shield != null){
			shield.SetActive(false);
		}
		isShieldActive = false;
	}

	public void JumpCounter(){
		removeUses(jump_cost);
		print ("Jump - Uses left: " + uses_left);
	}
}
