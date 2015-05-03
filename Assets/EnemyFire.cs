using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour {
		
	public GameObject ammo;
	public float ammoSize;
	public float ammoDistanceFromShooter;
	public float waitUntilFire;
	public float delayBetweenShots;

	// Update is called once per frame
	void Awake () {
		StartCoroutine(fireWait (waitUntilFire));
	}

	void fire(){
		Vector3 tempVec = transform.position;
		tempVec.x -= ammoDistanceFromShooter;
		GameObject ammoInstance = (GameObject)Instantiate(ammo, tempVec, transform.rotation);
		ammoInstance.GetComponent<ForwardShot>().SendMessage("setDirection", 1);
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
