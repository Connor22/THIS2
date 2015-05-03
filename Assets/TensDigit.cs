using UnityEngine;
using System.Collections;

public class TensDigit : MonoBehaviour {

	public Sprite[] spritesheet;

	void updateDigit(int value){
		GetComponent<SpriteRenderer>().sprite = spritesheet[value];
	}
}
