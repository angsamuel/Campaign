using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Environment {
	Character leader;
	int population;
	List<Army> armies;

	void Awake(){
		armies = new List<Army>();
		armies.Add(new Army());
		armies.Add (new Army ());
	}

	void Start () {
		base.Start ();
		NameWizard nameWizard = GameObject.Find ("NameWizard").GetComponent<NameWizard> ();
		name = nameWizard.GenerateCityName ();
		type = "city";
		leader = new Character();
	}

	public void FillArmySelectCB(){
		

		List<string> armyNames = new List<string>();
		Debug.Log (armies.Count);

		for (int i = 0; i<armies.Count; ++i) {
			armyNames.Add (armies[i].leader.firstName + " " + armies[i].leader.lastName);
		}
		GameObject armySelectCB = GameObject.Find ("ArmySelectCB") as GameObject;
		armySelectCB.GetComponent<Kender.uGUI.ComboBox> ().ClearItems ();
		armySelectCB.GetComponent<Kender.uGUI.ComboBox>().AddItems(armyNames);
	
	}

	void Update(){

	}


}