using UnityEngine;
using System.Collections;

public class SaltFlatsScript : Location {

    void Awake()
    {
        base.Awake();
        base.Start();
        name = "Salt Flats";
        type = "salt flats";
        population = Random.Range(0, 11);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
