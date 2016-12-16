using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIBank : MonoBehaviour {
	Vector3 dumpPosition = new Vector3 (500, 500, 500);

	List<GameObject> panels;
	public bool mouseOnUI = false;

	private GameObject playerCity;

    public GameObject cursor;
	public GameObject mainPanel;
	public GameObject nameWizard;

	//InfoPanel-----------------
	public GameObject infoPanel;
	public Text selectionNameText;
	public Text selectionTypeText;

	public GameObject warPanel;

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
