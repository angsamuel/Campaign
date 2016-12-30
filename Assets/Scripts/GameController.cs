using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


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
    public List<EnvironmentStruct> environmentStructList;
    //public List<int> environmentStructList;

    public int ppu = 32;

	public int cityNumber;

    public List<Environment> environmentList; //used for serialization

    //Environment Save Data
    public class JsonEnvironments
    {
        public EnvironmentStruct[] jsonEnvironments;
    }

    public enum EnvironmentEnum
    {
        SaltFlats
    };

    [Serializable]
    public class EnvironmentStruct
    {
        public string name;
        public EnvironmentEnum type;
        public int posX;
        public int posY;
        public int population;
    }

	//Player Data
	public GameObject playerCity;

	List<Vector3> freeCoordinates = new List<Vector3>();

    public List<City> cityList;

    void Awake()
    {
        cityList = new List<City>();
        day = 0;
        isDay = true;
        saltFlats = Resources.Load("Prefabs/SaltFlats") as GameObject;
        environmentStructList = new List<EnvironmentStruct>();
        //environmentStructList = new List<int>();
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
        for (int i = 0; i < freeCoordinates.Count; i++)
        {
            Vector3 temp = freeCoordinates[i];
            int randomIndex = UnityEngine.Random.Range(0, freeCoordinates.Count);
            freeCoordinates[i] = freeCoordinates[randomIndex];
            freeCoordinates[randomIndex] = temp;
        }

        //spawn player city
        int px = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
        int py = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
        playerCity = Instantiate(city, new Vector3(px, py, 99), Quaternion.identity) as GameObject;
        playerCity.GetComponent<City>().position.x = (int)freeCoordinates[0].x;
        playerCity.GetComponent<City>().position.y = (int)freeCoordinates[0].y;
        grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = playerCity;
        freeCoordinates.RemoveAt(0);
        playerCity.GetComponent<City>().type = "your home";

        for (int i = 0; i < 2; ++i)
        {
            playerCity.GetComponent<City>().CreateArmy();
        }

        playerCity.GetComponent<City>().FillArmySelectCB();


        //spawn other cities
        for (int i = 1; i < cityNumber; ++i)
        {
            SpawnCity();
        }

        if (!Directory.Exists(Application.dataPath + "/Saves/Test")) //generate world if it does not exist
        {
            while (freeCoordinates.Count > 0)
            {
                SpawnEnvironment((int)freeCoordinates[0].x, (int)freeCoordinates[0].y, saltFlats);
                freeCoordinates.RemoveAt(0);
            }

            //create save file
            Directory.CreateDirectory(Application.dataPath + "/Saves/Test");
            JsonEnvironments efj = new JsonEnvironments();
            efj.jsonEnvironments = environmentStructList.ToArray();

            string jsonEnvironmentsString = JsonUtility.ToJson(efj);
            Debug.Log(jsonEnvironmentsString + "complete");
            Debug.Log(efj.jsonEnvironments.Length);
            System.IO.File.WriteAllText(Application.dataPath + "/Saves/Test/Environment.json", jsonEnvironmentsString);
        }
        else //load world from files
        {
            string environmentFileString = System.IO.File.ReadAllText(Application.dataPath + "/Saves/Test/Environment.json");
            JsonEnvironments efj = JsonUtility.FromJson<JsonEnvironments>(environmentFileString);
            SpawnEnvironmentsFromJson(efj);
        }
    }

    // Update is called once per frame
    void Update () {
	}

    private Environment SpawnEnvironment(int x, int y, GameObject g)
    {
        int px = (int)grid[x, y].transform.position.x;
        int py = (int)grid[x, y].transform.position.y;
        GameObject spawnedEnvironment = Instantiate(g, new Vector3(px, py, 99), Quaternion.identity) as GameObject;
        grid[x,y].GetComponent<Tile>().environment = spawnedEnvironment;


        Environment gEnvironment = spawnedEnvironment.GetComponent<Environment>();
        EnvironmentStruct e = new EnvironmentStruct();
        e.name = gEnvironment.name;
        e.type = EnvironmentEnum.SaltFlats;
        e.population = gEnvironment.population;
        gEnvironment.position.x = x;
        gEnvironment.position.y = y;
        e.posX = x;
        e.posY = y;
        environmentStructList.Add(e);

        return g.GetComponent<Environment>();
    }

    private void SpawnEnvironmentsFromJson(JsonEnvironments efj)
    {
        //spawn correct environments in
        for(int i = 0; i< efj.jsonEnvironments.Length; i++)
        {
            EnvironmentStruct e = efj.jsonEnvironments[i];
            switch (e.type)
            {
                case EnvironmentEnum.SaltFlats:
                    SpawnEnvironment(e.posX, e.posY, saltFlats);
                    grid[e.posX, e.posY].GetComponent<Tile>().environment.GetComponent<Environment>().population = e.population;
                    break;
                default:
                    break;
            }
        }

   }

    private void SpawnCity()
    {
        int sx = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
        int sy = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
        GameObject spawnedCity = Instantiate(city, new Vector3(sx, sy, 99), Quaternion.identity) as GameObject;
        grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = spawnedCity;
        freeCoordinates.RemoveAt(0);
        cityList.Add(spawnedCity.GetComponent<City>());

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
        for (int i = 0; i < cityList.Count; ++i)
        {
            cityList[i].TakeTurn();
        }
        isDay = !isDay;
        for (int i = 0; i < cityList.Count; ++i)
        {
            cityList[i].RemoveIfDestroyed();
        }

        if (cityList.Count < 1)
        {
            Debug.Log("You Win!");
        }
        uiBank.selectedTile.SimulateMouseClick();

    }
}
