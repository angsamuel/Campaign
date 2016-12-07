using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
    public Vector3 dumpPosition = new Vector3(-1000, -1000, -1000);
	// Use this for initialization
	void Start () {
        transform.position = dumpPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
