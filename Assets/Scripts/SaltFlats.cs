using UnityEngine;
using System.Collections;

public class SaltFlats : Environment {

	// Use this for initialization
	void Start () {
        base.Start();
        name = "Salt Flats";
        type = "salt flats";
        population = Random.Range(0,11);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
