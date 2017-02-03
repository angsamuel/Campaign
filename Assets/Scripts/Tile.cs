using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
[Serializable]
public class Tile : MonoBehaviour {

    /* scans for mouse cursor
     * stores a single object (village, forrest, etc)
     * opens menu to activate object
     * stores location in grid
     * */

	public int posX;
	public int posY;

	private bool selected = false;
    public GameObject environment; //village forest etc.
    UIBank uiBank;

    public Army occupant;
    public bool isOccupied;

	// Use this for initialization 
	void Awake () {
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
        uiBank = uiBankObject.GetComponent<UIBank>();
        isOccupied = false;
	}
    void Start() { }
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			if (Input.GetMouseButtonDown (0)) {
				OnMouseClick ();
			}
		}
	}
    void OnMouseOver()
    {
		if (!uiBank.mouseOnUI && Input.mousePosition.x > uiBank.basePanel.GetComponent<RectTransform>().sizeDelta.x){
                selected = true;
            }
    }
    void OnMouseExit()
    {
		selected = false;
    }

	public void OnMouseClick(){
		if (selected && !uiBank.mouseOnUI) {
			SelectTile ();
		}
	}

	public void MoveCursorToMe(){
		uiBank.test = UnityEngine.Random.Range(0, 1000000) ;
		uiBank.cursor.transform.position = new Vector3 (transform.position.x, transform.position.y, 91);
		uiBank.selectionCoordText.text = "(" + posX.ToString() + ", " + posY.ToString() + ")";
		uiBank.selectedX = posX;
		uiBank.selectedY = posY;
		uiBank.selectedTile = this;
	}

	public void SelectTile(){
		MoveCursorToMe ();
	
		if (environment != null) {
			uiBank.selectionNameText.text = environment.GetComponent<Environment> ().name;
			uiBank.selectionTypeText.text = environment.GetComponent<Environment> ().type;
			uiBank.targetLocationText.text = environment.GetComponent<Environment>().name;
			uiBank.selectionPopText.text = environment.GetComponent<Environment>().population.ToString();
			uiBank.selectionOwnerText.text = environment.GetComponent<Environment>().owner;
			if (environment.GetComponent<Environment>().leader != null)
			{
				uiBank.selectionLeaderText.text = environment.GetComponent<Environment>().leader.firstName + " " + environment.GetComponent<Environment>().leader.lastName;
			}else
			{
				uiBank.selectionLeaderText.text = "N/A";
			}
		} else {
			uiBank.selectionNameText.text = "NULL";
			uiBank.selectionTypeText.text = "NULL";
			uiBank.targetLocationText.text = "NULL";
		}
		if(occupant != null)
		{
			uiBank.selectionArmyText.text = occupant.GetComponent<Army>().leader.firstName + " " + occupant.GetComponent<Army>().leader.lastName;
		}else
		{
			uiBank.selectionArmyText.text = "NULL";
		}
		//Display Territory number
		if (environment.GetComponent<Environment> ().type == "city" || environment.GetComponent<Environment> ().type == "your home") {
			uiBank.selctionTerritoriesText.text = environment.GetComponent<City> ().lands.Count.ToString();
		} else {
			uiBank.selctionTerritoriesText.text = "N/A";
		}
	}

    public void SimulateMouseClick()
    {
        if (selected)
        {
            OnMouseClick();
        }else
        {
            selected = true;
            OnMouseClick();
            selected = false;
        }
    }

    public void MakeSelected()
    {
        uiBank.cursor.transform.position = new Vector3(transform.position.x, transform.position.y, 91);
        uiBank.selectionCoordText.text = "(" + posX.ToString() + ", " + posY.ToString() + ")";
        uiBank.selectedTile = this;
        if (environment != null)
        {
            Debug.Log(environment.name);
            uiBank.selectionNameText.text = environment.GetComponent<Environment>().name;
            uiBank.selectionTypeText.text = environment.GetComponent<Environment>().type;
            uiBank.targetLocationText.text = environment.GetComponent<Environment>().name;
        }
        else
        {
            uiBank.selectionNameText.text = "NULL";
            uiBank.selectionTypeText.text = "NULL";
            uiBank.targetLocationText.text = "NULL";
        }
        if (occupant != null)
        {
            uiBank.selectionArmyText.text = occupant.GetComponent<Army>().leader.firstName + " " + occupant.GetComponent<Army>().leader.lastName;
        }
        else
        {
            uiBank.selectionArmyText.text = "NULL";
        }
    }
}
