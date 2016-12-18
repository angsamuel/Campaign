using UnityEngine;
using System.Collections;

public class Army : MonoBehaviour {

	public Character leader;
	public int soldiers;
	Vector3 dumpPosition;
	int zCoord = 97;

	void Awake(){
		dumpPosition = new Vector3 (1000, 1000, -1000);
		leader = new Character ();
	}

	// Use this for initialization
	void Start () {
		leader.profession = "general";
		soldiers = 0;
		float newZ = transform.position.z + 1f;
		transform.FindChild ("Background").transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetColorBackground(Color c){
		c.a = .8f;
		c.r -= .3f;
		c.g -= .3f;
		c.b -= .3f;
		transform.FindChild ("Background").GetComponent<SpriteRenderer> ().color = c;
	}

	public void SetColorFigure(Color c){
		GetComponent<SpriteRenderer> ().color = c;
	}

	public void SetColor(Color c){
		SetColorBackground (c);
		SetColorFigure (c);
	}

	public void TeleportOffScreen(){
		transform.position = dumpPosition;
	}


}
