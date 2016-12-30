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
		if (selected) {
            uiBank.test = UnityEngine.Random.Range(0, 1000000) ;
            uiBank.cursor.transform.position = new Vector3 (transform.position.x, transform.position.y, 91);
            uiBank.selectionCoordText.text = "(" + posX.ToString() + ", " + posY.ToString() + ")";
            uiBank.selectedX = posX;
            uiBank.selectedY = posY;
            uiBank.selectedTile = this;

           // uiBank.selectedTile = GetComponent<GameObject>();
			if (environment != null) {
				Debug.Log (environment.name);
				uiBank.selectionNameText.text = environment.GetComponent<Location> ().name;
				uiBank.selectionTypeText.text = environment.GetComponent<Location> ().type;
                uiBank.targetLocationText.text = environment.GetComponent<Location>().name;
                uiBank.selectionPopText.text = environment.GetComponent<Location>().population.ToString();
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
            uiBank.selectionNameText.text = environment.GetComponent<Location>().name;
            uiBank.selectionTypeText.text = environment.GetComponent<Location>().type;
            uiBank.targetLocationText.text = environment.GetComponent<Location>().name;
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
