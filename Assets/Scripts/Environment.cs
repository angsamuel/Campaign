using UnityEngine;
using System.Collections;
using System;
using System.IO;

[Serializable]
public class Environment : MonoBehaviour {

	public string name;
	public string type;
	protected GameObject uiBank;
    public int population = 0;
    public Vector2 position;
    public int posZ = 99;

    protected void Awake()
    {
        population = UnityEngine.Random.Range(0, 11);
        uiBank = GameObject.Find("UIBank");
        name = "null";
        position = new Vector2(0, 0);
        type = "null";
    }

	// Use this for initialization
	protected void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
	
	}

}
