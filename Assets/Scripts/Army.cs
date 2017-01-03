using UnityEngine;
using System.Collections;
using System;
using System.IO;
[Serializable]
public class Army : MonoBehaviour {
    public Vector2 position = new Vector2(1000, 1000);
    public int posZ = 97;

    Vector2 objectiveLocation;

    GameController gameController;

	public Character leader;
	public int soldiers;

	public Vector3 dumpPosition;

    public City rulerCity;

    public bool isStored = true;

    public void SetRulerCity(City c)
    {
        c.armies.Remove(this);
        c.armies.Add(this);
        rulerCity = c;
    }

    void Awake(){
        position = new Vector2();
		dumpPosition = new Vector3 (1000, 1000, -1000);
		leader = new Character ();
        soldiers = UnityEngine.Random.Range(10, 20);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        leader.profession = "general";
        float newZ = transform.position.z + 1f;
        transform.FindChild("Background").transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        primaryObjective = Objective.none;
        primaryObjectiveLoc = new Vector2(-1000, -1000);
        secondaryObjective = Objective.none;
        secondaryObjectiveLoc = new Vector2(-1000, -1000);
    }

	// Use this for initialization
	void Start () {

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
        Debug.Log(x + "," + y);
        if (!gameController.grid[x, y].GetComponent<Tile>().isOccupied)
        {
            gameController.grid[(int)position.x, (int)position.y].GetComponent<Tile>().isOccupied = false;
            gameController.grid[(int)position.x, (int)position.y].GetComponent<Tile>().occupant = null;

            position.x = x;
            position.y = y;

            gameController.grid[x, y].GetComponent<Tile>().occupant = this;
            gameController.grid[x, y].GetComponent<Tile>().isOccupied = true;
           
            transform.position = new Vector3(gameController.grid[x, y].GetComponent<Tile>().transform.position.x, gameController.grid[x, y].GetComponent<Tile>().transform.position.y, posZ);
        }
        Debug.Log(gameController.grid[x, y].GetComponent<Tile>().occupant.GetComponent<Army>().leader.firstName);
    }

    public void MoveAdjacent(int x, int y)
    {
        if (!gameController.grid[(int)position.x + x, (int)position.y + y].GetComponent<Tile>().isOccupied)
        {
            Move((int)position.x + x, (int)position.y + y);
        }else
        {
            int m = UnityEngine.Random.Range(-1, 2);
            if (x !=0)
            {
                Move((int)position.x, (int)position.y + m);
            }else if(y != 0)
            {
                Move((int)position.x + m, (int)position.y);
            }
        }
    }

    public enum Objective { none, moveTo, capture, ravage, returnHome };
    public Objective primaryObjective;
    public Vector2 primaryObjectiveLoc = new Vector2(-1,-1);
    public Objective secondaryObjective;
    public Vector2 secondaryObjectiveLoc = new Vector2(-1, -1);

    //army proceeds towards objective
    public void TakeAction()
    {
        Debug.Log("My Position: " + position.x + ", " + position.y);
        Debug.Log("Objective position " + primaryObjectiveLoc.x + "," + primaryObjectiveLoc.y);
        if(primaryObjective == Objective.none)
        {
            primaryObjective = secondaryObjective;
            secondaryObjective = Objective.none;
            primaryObjectiveLoc = secondaryObjectiveLoc;
            secondaryObjectiveLoc = new Vector2();
        }

        if (primaryObjective != Objective.none)
        {
            if (isStored)
            {
                rulerCity.storedArmies.Remove(this);
                isStored = false;
                Move((int)rulerCity.position.x, (int)rulerCity.position.y);
            }
            else if (position.x < primaryObjectiveLoc.x && position.y < primaryObjectiveLoc.y)
            {
                int upOrSide = UnityEngine.Random.Range(0, 2);
                switch (upOrSide)
                {
                    case 0:
                        MoveAdjacent(1, 0);
                        break;
                    case 1:
                        MoveAdjacent(0, 1);
                        break;
                    default:
                        break;
                }
            }else if (position.x > primaryObjectiveLoc.x && position.y > primaryObjectiveLoc.y)
            {
                int upOrSide = UnityEngine.Random.Range(0, 2);
                switch (upOrSide)
                {
                    case 0:
                        MoveAdjacent(-1, 0);
                        break;
                    case 1:
                        MoveAdjacent(0, -1);
                        break;
                    default:
                        break;
                }
            }else if(position.x > primaryObjectiveLoc.x){
                MoveAdjacent(-1, 0);
            }else if(position.y > primaryObjectiveLoc.y){
                MoveAdjacent(0, -1);
            }else if(position.x < primaryObjectiveLoc.x){
                MoveAdjacent(1, 0);
            }else if(position.y < primaryObjectiveLoc.y){
                MoveAdjacent(0, 1);
            }else if(position.x == primaryObjectiveLoc.x && position.y == primaryObjectiveLoc.y){
                ExecutePrimaryObjective();
            }
        }
    }

    public void ExecutePrimaryObjective()
    {
        switch (primaryObjective)
        {
            case Objective.moveTo:
                break;
            case Objective.returnHome:
                rulerCity.StoreArmy(this);
                break;
            case Objective.ravage:
                gameController.grid[(int)position.x, (int)position.y].GetComponent<Tile>().environment.GetComponent<Environment>().population = 0;
                break;
            default:
                break;
        }
        primaryObjective = Objective.none;
    }

    public void OrderDeployTo(int x, int y, bool primary)
    {
        if (primary)
        {
            primaryObjective = Objective.moveTo;
            primaryObjectiveLoc = new Vector2(x, y);
        }else {
            secondaryObjective = Objective.moveTo;
            secondaryObjectiveLoc = new Vector2(x, y);
        }
    }

    public void OrderRavage(int x, int y, bool primary) {
        if (primary)
        {
            primaryObjective = Objective.ravage;
            primaryObjectiveLoc = new Vector2(x, y);
        }else
        {
            secondaryObjective = Objective.ravage;
            secondaryObjectiveLoc = new Vector2(x, y);
        }
    }

   public void OrderReturnHome(bool primary)
    {
        if (primary)
        {
            primaryObjective = Objective.returnHome;
            primaryObjectiveLoc = rulerCity.position;
        }else
        {
            secondaryObjective = Objective.returnHome;
            secondaryObjectiveLoc = rulerCity.position;
        }
    }
}
