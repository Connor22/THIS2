using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<PlayerChecks>().quitProcess();
	}
}
