using UnityEngine;
using System.Collections;
using System;
using System.IO;

[Serializable]
public class SaltFlats : Environment {

    void Awake()
    {
        base.Awake();
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
