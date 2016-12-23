using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Environment {

    public Vector2 position;
	GameObject army;

	Character leader;
	int population;
	public List<GameObject> armies;

	List<GameObject> storedArmies;

    public Hashtable armyTable;

    UIBank uiBank;

	void Awake(){
        position = new Vector2();
        armyTable = new Hashtable();
		army = Resources.Load ("Prefabs/Army") as GameObject;
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
		uiBank = uiBankObject.GetComponent<UIBank> ();
		storedArmies = new List<GameObject> ();
		armies = new List<GameObject>();

        type = "city";

        // pick a random color
        float R = Random.Range(.4f, .9f);
        float G = Random.Range(.4f, .9f);
        float B = Random.Range(.4f, .9f);
        Color newColor = new Color(R, G, B, 1.0f);

        // apply it on current object's material
        GetComponent<Renderer>().material.color = newColor;

       
    }

    void Start () {
		base.Start ();
        NameWizard nameWizard = GameObject.Find("NameWizard").GetComponent<NameWizard>();
        name = nameWizard.GenerateCityName();
        leader = new Character();
    }

	public void FillArmySelectCB(){
		
		List<string> armyNames = new List<string>();

		for (int i = 0; i<armies.Count; ++i) {
			armyNames.Add (armies[i].GetComponent<Army>().leader.firstName + " " + armies[i].GetComponent<Army>().leader.lastName);
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

	public void CreateArmy(){
		GameObject a = Instantiate (army,new Vector3(transform.position.x, transform.position.y, 97), Quaternion.identity) as GameObject;
        //BANDAID!!!
        a.GetComponent<Army>().position.x = position.x;
        a.GetComponent<Army>().position.y = position.y;
		a.GetComponent<Army> ().SetColor (GetComponent<Renderer> ().material.color);
		a.GetComponent<Army> ().TeleportOffScreen ();
		storedArmies.Add (a);
		armies.Add (a);
        armyTable.Add(a.GetComponent<Army>().leader.firstName + " " + a.GetComponent<Army>().leader.lastName, a);
	}

    public void TakeTurn()
    {
        //tell all armies to take action
        for(int i = 0; i<armies.Count; i++)
        {
            armies[i].GetComponent<Army>().TakeAction();
        }



    }


}