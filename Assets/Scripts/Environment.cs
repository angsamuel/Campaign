using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public string name;
	public string type;
	protected GameObject uiBank;

	// Use this for initialization
	protected void Start () {
		uiBank = GameObject.Find ("UIBank");
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
