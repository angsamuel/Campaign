using UnityEngine;
using System.Collections;

public class City : Environment {

	// Use this for initialization
	void Start () {
		base.Start ();
		name = uiBank.GetComponent<UIBank> ().nameWizard.GetComponent<NameWizard> ().GenerateCityName();
		type = "City";
		Debug.Log (name);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
