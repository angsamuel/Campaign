using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public string name;
	public string type;
	protected GameObject uiBank;
    public int population = 0;

	// Use this for initialization
	protected void Start () {
		uiBank = GameObject.Find ("UIBank");
		name = "";
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
