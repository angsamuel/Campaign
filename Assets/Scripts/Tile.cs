using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    /* scans for mouse cursor
     * stores a single object (village, forrest, etc)
     * opens menu to activate object
     * 
     * */

    private GameObject environment; //village forest etc.
    UIBank uiBank;

	// Use this for initialization 
	void Start () {
        uiBank = GameObject.Find("UIBank").GetComponent<UIBank>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
		if (!uiBank.mainPanel.GetComponent<MainPanelScript> ().mouseOver) {
			uiBank.cursor.transform.position = new Vector3 (transform.position.x, transform.position.y, -9);
		}
    }
    void OnMouseExit()
    {
        uiBank.cursor.transform.position = uiBank.cursor.GetComponent<Cursor>().dumpPosition;
    }
}
