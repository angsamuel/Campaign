using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
    /* Create World
     * Manage Turns
     * Check For Win Condition
     * store player city
     * */

    public int height;
    public int width;

    public int day;
    public bool isDay;

	GameObject tile;
	GameObject city;
    GameObject saltFlats;

	UIBank uiBank;
    
	public GameObject[,] grid;
    public int ppu = 32;

	public int cityNumber;

	//Player Data
	public GameObject playerCity;

	List<Vector3> freeCoordinates = new List<Vector3>();

    List<GameObject> cityList;

    void Awake()
    {
        cityList = new List<GameObject>();
        day = 0;
        isDay = true;
        saltFlats = Resources.Load("Prefabs/SaltFlats") as GameObject;
    }

    // Use this for initialization
    void Start () {
        CreateWorld();
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
		uiBank.OpenInfoPanel ();
	}

    private void CreateWorld()
    {
		//spawn tiles
        Debug.Log(((Screen.height / 32f) - height) / 2f);
        grid = new GameObject[width, height];
        tile = Resources.Load("Prefabs/Tile") as GameObject;
		city = Resources.Load ("Prefabs/City") as GameObject;

        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
				freeCoordinates.Add(new Vector3(ix, iy, -1));
				float xLoc = ix - (width/2);
				float yLoc = iy - (height/2);
				GameObject spawnedTile = Instantiate(tile, new Vector3(xLoc, yLoc, 100), Quaternion.identity) as GameObject;
                grid[ix, iy] = spawnedTile;

				//give tile coordinate information
				spawnedTile.GetComponent<Tile> ().posX = ix;
				spawnedTile.GetComponent<Tile> ().posY = iy;
            }
        }

		//shuffle coordinates
		for (int i = 0; i < freeCoordinates.Count; i++) {
			Vector3 temp = freeCoordinates [i];
			int randomIndex = Random.Range (0, freeCoordinates.Count);
			freeCoordinates [i] = freeCoordinates [randomIndex];
			freeCoordinates [randomIndex] = temp;
		}

		//spawn player city
		int px = (int)grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].transform.position.x; 
		int py = (int)grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].transform.position.y;

        playerCity = Instantiate (city,new Vector3(px, py, 99), Quaternion.identity) as GameObject;

        playerCity.GetComponent<City>().position.x = (int)freeCoordinates[0].x;
        playerCity.GetComponent<City>().position.y = (int)freeCoordinates[0].y;

        grid[(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].GetComponent<Tile> ().environment = playerCity;
       // grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().MakeSelected();

        freeCoordinates.RemoveAt(0);

        playerCity.GetComponent<City>().type = "your home";
        
        for (int i = 0; i < 2; ++i)
        {
            playerCity.GetComponent<City>().CreateArmy();
        }

        playerCity.GetComponent<City>().FillArmySelectCB();

        //playerCity.GetComponent<City>().armies[0].GetComponent<Army>(). OrderMoveTo(0,0);

        //spawn other cities
        for (int i = 1; i < cityNumber; ++i) {
            SpawnCity();
		}

        while(freeCoordinates.Count > 0)
        {
            SpawnEnvironment((int)freeCoordinates[0].x, (int)freeCoordinates[0].y, saltFlats);
        }
    }

	// Update is called once per frame
	void Update () {
	}

    private void SpawnEnvironment(int x, int y, GameObject g)
    {
        int px = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
        int py = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
        GameObject spawnedEnvironment = Instantiate(g, new Vector3(px, py, 99), Quaternion.identity) as GameObject;
        grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = spawnedEnvironment;
        freeCoordinates.RemoveAt(0);
    }

    private void SpawnCity()
    {
        int sx = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
        int sy = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
        GameObject spawnedCity = Instantiate(city, new Vector3(sx, sy, 99), Quaternion.identity) as GameObject;
        grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = spawnedCity;
        freeCoordinates.RemoveAt(0);
        cityList.Add(spawnedCity);

        //create armies
        for (int i = 0; i < 2; ++i)
        {
            spawnedCity.GetComponent<City>().CreateArmy();
        }
    }
	public int GetMapRows(){
		return height;
	}
	public int GetMapCols(){
		return width;
	}

    public void AdvanceTime()
    {
        //player city takes turn
        playerCity.GetComponent<City>().TakeTurn();
        for (int i = 0; i < playerCity.GetComponent<City>().armies.Count; ++i)
        {

        }
        isDay = !isDay;
    }
}
