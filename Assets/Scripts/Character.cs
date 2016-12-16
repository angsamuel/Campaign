using UnityEngine;
using System.Collections;

public class Character {
	
	public string firstName;
	public string lastName;
	public string profession;
	//maybe stats later

	// Use this for initialization
	public Character () {
		NameWizard nameWizard = GameObject.Find ("NameWizard").GetComponent<NameWizard> ();
		int gender = Random.Range (0, 2);
		if (gender == 0) {
			firstName = nameWizard.RandomFemaleName ();
		} else {
			firstName = nameWizard.RandomMaleName ();
		}
		lastName = nameWizard.RandomMaleName ();
		profession = "vagrant";
	}
		
}
