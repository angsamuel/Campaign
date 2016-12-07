using UnityEngine;
using System.Collections;

public class MainPanelScript : MonoBehaviour {
	public bool mouseOver = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver(){
		mouseOver = true;
		Debug.Log("hey!!!");
	}

	void OnMouseExit(){
		mouseOver = false;
	}


}
