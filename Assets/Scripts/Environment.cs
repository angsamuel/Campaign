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
    public string owner = "";
    public Character leader;

    protected void Awake()
    {
        population = UnityEngine.Random.Range(0, 11);
        uiBank = GameObject.Find("UIBank");
        name = "null";
        position = new Vector2(0, 0);
        type = "null";
        owner = "N/A";
    }

	// Use this for initialization
	protected void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	
	}

    public void ConnectToOwner()
    {
        GameObject gmo = GameObject.Find("GameController") as GameObject;
        GameController gameController = gmo.GetComponent<GameController>();
       // Debug.Log(owner);
        if (gameController.cityTable.ContainsKey(owner))
        {
            Debug.Log(owner);
            
            (gameController.cityTable[owner] as City).lands.Add(this);
            GetComponent<Renderer>().material.color = (gameController.cityTable[owner] as City).GetComponent<Renderer>().material.color;
            Debug.Log((gameController.cityTable[owner] as City).GetComponent<Renderer>().material.color.r);
        }
    }
}
