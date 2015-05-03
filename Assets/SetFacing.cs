using UnityEngine;
using System.Collections;

public class SetFacing : MonoBehaviour {

	public bool defaultIsRight;
	public bool faceRight;

	// Use this for initialization
	void Start () {
		if (defaultIsRight != faceRight)
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
