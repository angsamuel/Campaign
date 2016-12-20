using UnityEngine;
using System.Collections;

public class Army : MonoBehaviour {
    int posX;
    int posY;
    int posZ = 97;

    GameController gameController;

	public Character leader;
	public int soldiers;
	Vector3 dumpPosition;

	void Awake(){
		dumpPosition = new Vector3 (1000, 1000, -1000);
		leader = new Character ();
        soldiers = Random.Range(10, 20);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

	// Use this for initialization
	void Start () {
		leader.profession = "general";
		float newZ = transform.position.z + 1f;
		transform.FindChild ("Background").transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        Move(Random.Range(0, 19), Random.Range(0, 19));
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

    //moves army to specified coordinate
    public void Move(int x, int y)
    {
        if (!gameController.grid[x, y].GetComponent<Tile>().isOccupied)
        {
            gameController.grid[x, y].GetComponent<Tile>().occupant = this;
            gameController.grid[x, y].GetComponent<Tile>().isOccupied = true;
            transform.position = new Vector3(gameController.grid[x, y].GetComponent<Tile>().transform.position.x, gameController.grid[x, y].GetComponent<Tile>().transform.position.y, posZ);
            posX = x;
            posY = y;
            if(gameController.grid[posX, posY].GetComponent<Tile>().isOccupied)
            {
                gameController.grid[posX, posY].GetComponent<Tile>().isOccupied = false;
            }
        }
        Debug.Log(gameController.grid[x, y].GetComponent<Tile>().occupant.GetComponent<Army>().leader.firstName);
    }


}
