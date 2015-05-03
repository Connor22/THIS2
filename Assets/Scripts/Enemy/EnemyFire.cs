using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour {
		
	public GameObject ammo;
	public float ammoSize;
	public float ammoDistanceFromShooter;
	public float waitUntilFire;
	public float delayBetweenShots;

	private SetFacing direction;
	

	// Update is called once per frame
	void Awake () {
		direction = GetComponent<SetFacing>();
		StartCoroutine(fireWait (waitUntilFire));
	}

	void fire(){
		Vector3 tempVec = transform.position;
		int facing = 1;
		if(direction.faceRight){
			tempVec.x += ammoDistanceFromShooter;
			facing = -1;
		} else {
			tempVec.x -= ammoDistanceFromShooter;
		}
		GameObject ammoInstance = (GameObject)Instantiate(ammo, tempVec, transform.rotation);
		ammoInstance.GetComponent<ForwardShot>().SendMessage("setDirection", facing);
		Vector3 ammoscale = ammoInstance.transform.localScale;
		ammoscale.x = ammoscale.y = ammoSize;
		ammoInstance.transform.localScale = ammoscale;
		StartCoroutine(fireWait());
	}

	IEnumerator fireWait(float seconds = 0){
		if (seconds == 0)
			seconds = delayBetweenShots;
		yield return new WaitForSeconds(seconds);
		fire ();
	}
}
