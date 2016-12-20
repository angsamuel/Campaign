using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	void Start () {
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
        uiBank = uiBankObject.GetComponent<UIBank>();
        isOccupied = false;
	}
	
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

	void OnMouseClick(){
		if (selected) {
			uiBank.cursor.transform.position = new Vector3 (transform.position.x, transform.position.y, 91);
            uiBank.selectionCoordText.text = "(" + posX.ToString() + ", " + posY.ToString() + ")";
            uiBank.selectedTile = GetComponent<GameObject>();
			if (environment != null) {
				Debug.Log (environment.name);
				uiBank.selectionNameText.text = environment.GetComponent<Environment> ().name;
				uiBank.selectionTypeText.text = environment.GetComponent<Environment> ().type;
                uiBank.targetLocationText.text = environment.GetComponent<Environment>().name;
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
}
