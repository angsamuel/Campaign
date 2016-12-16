using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Environment {
	GameObject army;

	Character leader;
	int population;
	List<GameObject> armies;

	void Awake(){
		army = Resources.Load ("Prefabs/Army") as GameObject;
		armies = new List<GameObject>();
	}

	void Start () {
		
		// pick a random color
		float R = Random.Range(.4f, .9f);
		float G = Random.Range (.4f, .9f);
		float B = Random.Range (.4f, .9f);
		Color newColor = new Color(R, G, B, 1.0f );

		// apply it on current object's material
		GetComponent<Renderer>().material.color = newColor; 

		//Spawn Army with Same Color
		GameObject a = Instantiate (army,new Vector3(transform.position.x, transform.position.y, 97), Quaternion.identity) as GameObject;
		a.GetComponent<Army> ().SetColor (GetComponent<Renderer> ().material.color);

		base.Start ();
		NameWizard nameWizard = GameObject.Find ("NameWizard").GetComponent<NameWizard> ();
		name = nameWizard.GenerateCityName ();
		type = "city";
		leader = new Character();
	}

	public void FillArmySelectCB(){
		

		List<string> armyNames = new List<string>();
		/*

		for (int i = 0; i<armies.Count; ++i) {
			armyNames.Add (armies[i].leader.firstName + " " + armies[i].leader.lastName);
		}
		GameObject armySelectCB = GameObject.Find ("ArmySelectCB") as GameObject;
		armySelectCB.GetComponent<Kender.uGUI.ComboBox> ().ClearItems ();
		armySelectCB.GetComponent<Kender.uGUI.ComboBox>().AddItems(armyNames);
	*/
	}

	void Update(){

	}


}