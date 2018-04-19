using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {

	string want;
	public Text wantTxt;

	int sMoney;
	public Text sMoneyTxt;

	public string[] wantArray;

	void Start() {
		want = wantArray [Random.Range (0, wantArray.Length)];
		sMoney = 0;
		wantTxt.text = want + ", please!";
		sMoneyTxt.text = "$" + sMoney;
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == want) {
			CycleWant ();
			sMoney += 1;
			sMoneyTxt.text = "$" + sMoney;

		}
	}

	void CycleWant() {
		want = wantArray [Random.Range (0, wantArray.Length)];
		Debug.Log (want);
		wantTxt.text = want + ", please!";
	}
}
