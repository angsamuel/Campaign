using UnityEngine;
using System.Collections;

public class SaltFlats : Environment {

    void Awake()
    {
        base.Awake();
        Debug.Log(name);
        name = "Salt Flats";
        type = "salt flats";
    }
	// Use this for initialization
	void Start () {
        base.Start();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
