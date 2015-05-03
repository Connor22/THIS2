using UnityEngine;
using System.Collections;

public class FinalTally : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		int uses_left_int = (int)GetComponent<PlayerChecks>().currentMaxUses;
		int hundreds = uses_left_int / 100;
		int tens = uses_left_int / 10 - hundreds * 10;
		int singles = uses_left_int - hundreds * 100 - tens * 10;
		GameObject.Find("Hundreds").GetComponent<DigitUpdater>().updateDigit(hundreds);
		GameObject.Find("Tens").GetComponent<DigitUpdater>().updateDigit(tens);
		GameObject.Find("Singles").GetComponent<DigitUpdater>().updateDigit(singles);
	}
}
