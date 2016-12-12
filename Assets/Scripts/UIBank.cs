using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBank : MonoBehaviour {
    public Text nameText;
    public GameObject cursor;
	public GameObject mainPanel;
	public GameObject nameWizard;


	public bool mouseOnUI = false;

	//InfoPanel-----------------
	public GameObject infoPanel;
	public Text selectionNameText;
	public Text selectionTypeText;



	//Panels
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DefaultSelection(){
		selectionNameText.text = "Lanada Rue";
		selectionTypeText.text = "The Land of Regret";

	}
}
