using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DigitUpdater : MonoBehaviour {
	
	public Sprite[] spritesheet;
	
	public void updateDigit(int value){
		gameObject.GetComponent<Image>().sprite = spritesheet[value];
	}
}
