using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    /* scans for mouse cursor
     * stores a single object (village, forrest, etc)
     * opens menu to activate object
     * 
     * */

	private bool selected = false;
    public GameObject environment; //village forest etc.
    UIBank uiBank;

	// Use this for initialization 
	void Start () {
		GameObject uiBankObject = GameObject.Find ("UIBank") as GameObject;
        uiBank = uiBankObject.GetComponent<UIBank>();
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
		if (!uiBank.mouseOnUI) {
			selected = true;
		}
    }
    void OnMouseExit()
    {
		selected = false;
    }

	void OnMouseClick(){
		if (selected) {
			uiBank.cursor.transform.position = new Vector3 (transform.position.x, transform.position.y, -9);
			Debug.Log ("Mouse Click");
			//uiBank.infoPanel.GetComponent<MainPanelScript> ().MakeActive ();
			if (environment != null) {
				//uiBank.infoPanel.transform.GetChild (0).GetComponent<Text> ().text = environment.GetComponent<Environment> ().name;
				//uiBank.infoPanel.transform.GetChild (1).GetComponent<Text> ().text = environment.GetComponent<Environment> ().type;
			} else {
				//uiBank.infoPanel.transform.GetChild (0).GetComponent<Text> ().text = "Nothing"; 
				//uiBank.infoPanel.transform.GetChild (1).GetComponent<Text> ().text = "";
			}
		}
	}
}
