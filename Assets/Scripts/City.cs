using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Location {
    GameController gameController;
    public Vector2 position;
	GameObject army;

	Character leader;
	public List<Army> armies;

	public List<Army> storedArmies;

    public Hashtable armyTable;

    UIBank uiBank;

	void Awake(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        position = new Vector2();
        armyTable = new Hashtable();
		army = Resources.Load ("Prefabs/Army") as GameObject;
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
		uiBank = uiBankObject.GetComponent<UIBank> ();
		storedArmies = new List<Army> ();
		armies = new List<Army>();

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
        population = Random.Range(10000, 15000);
    }

    //city checks itself to see if it can still be in the game, removes itself otherwise
    public void RemoveIfDestroyed()
    {
        if(population < 1)
        {
            gameController.cityList.Remove(this);
            type = "whisper";
        }
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

	public void StoreArmy(Army a){
        Debug.Log("army stored!");
        gameController.grid[(int)a.position.x, (int)a.position.y].GetComponent<Tile>().isOccupied = false;
        gameController.grid[(int)a.position.x, (int)a.position.y].GetComponent<Tile>().occupant = null;
        storedArmies.Add (a);
		a.TeleportOffScreen ();
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
		storedArmies.Add (a.GetComponent<Army>());
		armies.Add (a.GetComponent<Army>());
        armyTable.Add(a.GetComponent<Army>().leader.firstName + " " + a.GetComponent<Army>().leader.lastName, a);
        a.GetComponent<Army>().SetRulerCity(this);
	}

    public void TakeTurn()
    {
        //tell all armies to take action
        for(int i = 0; i<armies.Count; i++)
        {
            armies[i].GetComponent<Army>().TakeAction();
        }
        population += (int)(population * 0.015f);
    }
}