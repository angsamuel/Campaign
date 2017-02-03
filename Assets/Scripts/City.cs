using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class City : Environment {
    GameController gameController;
    public Vector2 position;
	public GameObject army;
	public int food;
	public int muns;

	public List<Army> armies;

	public List<Army> storedArmies;

    public List<Environment> lands;

    public Hashtable armyTable;

    UIBank uiBank;

	void Awake(){
		owner = "N/A";
        lands = new List<Environment>();
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

        NameWizard nameWizard = GameObject.Find("NameWizard").GetComponent<NameWizard>();
        name = nameWizard.GenerateCityName();
        leader = new Character();
        population = Random.Range(10000, 15000);
        muns = population / 3;
        food = population + (population / 3);
    }
    
    void Start () {
		base.Start ();
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

	private void RemoveLostVillages(){
		List<int> indexes = new List<int> ();
		int offset = 0;
		for (int i = 0; i < lands.Count; ++i) {
			if (lands [i].owner != name) {
				indexes.Add (i);
			}
		}
		for (int i = 0; i < indexes.Count; ++i) {
			lands.RemoveAt (indexes[i]+offset);
			offset++;
		}
	}

	public void FillArmySelectDropdown(){
		List<string> armyNames = new List<string> ();
		for (int i = 0; i<armies.Count; ++i) {
			armyNames.Add (armies[i].leader.firstName + " " + armies[i].leader.lastName);
		}
		Dropdown armySelectDropdown = uiBank.ArmySelectDropdown;
		armySelectDropdown.AddOptions (armyNames);
	}
	public void StoreArmy( Army a){
        Debug.Log("army stored!");
        gameController.grid[(int)a.position.x, (int)a.position.y].GetComponent<Tile>().isOccupied = false;
        gameController.grid[(int)a.position.x, (int)a.position.y].GetComponent<Tile>().occupant = null;
        a.isStored = true;
        storedArmies.Add (a);
		a.TeleportOffScreen ();
	}

	void Update(){
	}

	public Army CreateArmy(){
		GameObject a = Instantiate (army,new Vector3(transform.position.x, transform.position.y, 97), Quaternion.identity) as GameObject;
        //BANDAID!!!
        a.GetComponent<Army>().position.x = position.x;
        a.GetComponent<Army>().position.y = position.y;
		a.GetComponent<Army> ().SetColor (GetComponent<Renderer> ().material.color);
		a.GetComponent<Army> ().TeleportOffScreen ();
		storedArmies.Add (a.GetComponent<Army>());
		armies.Add (a.GetComponent<Army>());
        armyTable.Add(a.GetComponent<Army>().leader.firstName + " " + a.GetComponent<Army>().leader.lastName, a.GetComponent<Army>());
        a.GetComponent<Army>().SetRulerCity(this);
        return a.GetComponent<Army>();
	}

    public void ResetArmyTable()
    {
        armyTable.Clear();
        armyTable = new Hashtable();
        for(int i = 0; i<armies.Count; ++i)
        {
            Army a = armies[i];
            armyTable.Add(a.leader.firstName + " " + a.leader.lastName, a);
        }
    }

    public void TakeTurn()
    {
        //tell all armies to take action
        for(int i = 0; i<armies.Count; i++)
        {
            armies[i].GetComponent<Army>().TakeAction();
        }

        
        if (gameController.dayOfWeek == 7)
        {
            population += (int)(population * 0.015f);
        }

    }

	public void CollectFromVillages(){
		for (int i = 0; i < lands.Count; ++i) {
			food += lands [i].GetComponent<Village> ().ProduceFood();
			muns += lands [i].GetComponent<Village> ().ProduceMun();
		}

	}

}
	
