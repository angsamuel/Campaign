using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Environment {
	GameObject army;

	Character leader;
	int population;
	List<GameObject> armies;

	List<GameObject> storedArmies;

    public Hashtable armyTable;

    UIBank uiBank;


	void Awake(){
        armyTable = new Hashtable();
		army = Resources.Load ("Prefabs/Army") as GameObject;
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
		uiBank = uiBankObject.GetComponent<UIBank> ();
		storedArmies = new List<GameObject> ();
		armies = new List<GameObject>();

		//create armies
		for (int i = 0; i < 2; ++i) {
			CreateArmy ();
		}
        type = "city";
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
		base.Start ();
		NameWizard nameWizard = GameObject.Find ("NameWizard").GetComponent<NameWizard> ();
		name = nameWizard.GenerateCityName ();
		leader = new Character();
	
	}

	public void FillArmySelectCB(){
		
		List<string> armyNames = new List<string>();

		for (int i = 0; i<armies.Count; ++i) {
			armyNames.Add (armies[i].GetComponent<Army>().leader.firstName + " " + armies[i].GetComponent<Army>().leader.lastName);
			//Debug.Log(armies[i].GetComponent<Army>().leader.firstName + "|");
			//armyNames.Add("boi");
		}
		GameObject armySelectCB = uiBank.ArmySelectCB;
		armySelectCB.GetComponent<Kender.uGUI.ComboBox> ().ClearItems ();
		armySelectCB.GetComponent<Kender.uGUI.ComboBox>().AddItems(armyNames);
	}

	public void StoreArmy(GameObject a){
		storedArmies.Add (a);
		a.GetComponent<Army> ().TeleportOffScreen ();
	}

	void Update(){

	}

	void CreateArmy(){
		GameObject a = Instantiate (army,new Vector3(transform.position.x, transform.position.y, 97), Quaternion.identity) as GameObject;
		a.GetComponent<Army> ().SetColor (GetComponent<Renderer> ().material.color);
		a.GetComponent<Army> ().TeleportOffScreen ();
		storedArmies.Add (a);
		armies.Add (a);
        armyTable.Add(a.GetComponent<Army>().leader.firstName + " " + a.GetComponent<Army>().leader.lastName, a);
	}


}