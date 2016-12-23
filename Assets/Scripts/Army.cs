using UnityEngine;
using System.Collections;

public class Army : MonoBehaviour {
    public Vector2 position;
    int posZ = 97;

    Vector2 objectiveLocation;

    GameController gameController;

	public Character leader;
	public int soldiers;
	Vector3 dumpPosition;

    City rulerCity;

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
        soldiers = Random.Range(10, 20);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

	// Use this for initialization
	void Start () {
		leader.profession = "general";
		float newZ = transform.position.z + 1f;
		transform.FindChild ("Background").transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        //Move(Random.Range(0, 19), Random.Range(0, 19));
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
            int m = Random.Range(-1, 2);
            if (x !=0)
            {
                Move((int)position.x, (int)position.y + m);
            }else if(y != 0)
            {
                Move((int)position.x + m, (int)position.y);
            }
        }
    }

    enum Objective { none, moveTo, capture, returnHome };
    Objective primaryObjective;
    Vector2 primaryObjectiveLoc = new Vector2(-1,-1);
    Objective secondaryObjective;
    Vector2 secondaryObjectiveLoc = new Vector2(-1, -1);

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
            if (rulerCity.storedArmies.Contains(this))
            {
                rulerCity.storedArmies.Remove(this);
                Move((int)rulerCity.position.x, (int)rulerCity.position.y);
            }
            else if (position.x < primaryObjectiveLoc.x && position.y < primaryObjectiveLoc.y)
            {
                int upOrSide = Random.Range(0, 2);
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
                int upOrSide = Random.Range(0, 2);
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
                primaryObjective = Objective.none;
                break;
            case Objective.returnHome:
                rulerCity.StoreArmy(this);
                primaryObjective = Objective.none;
                break;
            default:
                break;
        }
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
