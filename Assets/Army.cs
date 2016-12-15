using UnityEngine;
using System.Collections;

public class Army {

	public Character leader;
	public int soldiers;

	// Use this for initialization
	 public Army () {
		leader = new Character ();
		leader.profession = "general";
		soldiers = 0;
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}