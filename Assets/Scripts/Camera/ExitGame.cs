﻿using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKey( KeyCode.Escape )){
			GetComponent<PlayerChecks>().quitProcess();
			GameObject.Find("Player").GetComponent<PlayerActions>().nullForm();
			Application.Quit();
		} else if (Input.GetKey( KeyCode.R )){
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
