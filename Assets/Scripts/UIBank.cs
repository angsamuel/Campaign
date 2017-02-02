using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIBank : MonoBehaviour {
	Vector3 dumpPosition = new Vector3 (500, 500, 500);

	List<GameObject> panels;
	public bool mouseOnUI = false;
    public int test = 0;

    public int selectedX;
    public int selectedY;
    public Tile selectedTile;
    private GameObject playerCity;

    public GameObject gameController;



    public GameObject cursor;
	public GameObject mainPanel;
    public GameObject basePanel;
    public GameObject nameWizard;

	//InfoPanel-----------------
	public GameObject infoPanel;
	public Text selectionNameText;
	public Text selectionTypeText;
    public Text selectionCoordText;
    public Text selectionArmyText;
    public Text selectionPopText;
    public Text selectionLeaderText;
    public Text selectionOwnerText;
	public Text selctionTerritoriesText;

	public GameObject warPanel;
	//public GameObject ArmySelectCB;
    public Dropdown ArmySelectDropdown;
     
    public Text soldiersNumText;
    public GameObject actionSelectCB;
	public Dropdown actionSelectDD;
	public Dropdown returnActionDD;
    public GameObject returnActionSelectCB;
    public GameObject orderDocument;

    public Text targetLocationText;

	//Time--------------------------
	public Text dayText;
	public Text weekText;



	//Panels
	// Use this for initialization
	void Start () {
		panels = new List<GameObject> (){ infoPanel, warPanel };
		//DisableAllPanels ();
		infoPanel.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DefaultSelection(){
		selectionNameText.text = "Lanada Rue";
		selectionTypeText.text = "The Land of Regret";
	}

	public void DisableAllPanels(){
		foreach (GameObject p in panels) {
			p.SetActive (false);
		}
	}
	public void OpenInfoPanel(){
		DisableAllPanels ();
		infoPanel.SetActive (true);
	}
	public void OpenWarPanel(){
		DisableAllPanels ();
		warPanel.SetActive (true);
	}
}
