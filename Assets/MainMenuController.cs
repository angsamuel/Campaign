using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("profile", "This will literally never be a profile");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void StartNewGame()
    {
        Application.LoadLevel("OverWorld");
    }

}
