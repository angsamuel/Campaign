﻿using UnityEngine;
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
    public bool gameReady = false;

    public int height;
    public int width;

    public int day;
    public bool isDay;

    //for saving and loading, not game logic
    public List<City> allCities;

    public GameObject tile;
    public GameObject city;
    public GameObject saltFlats;

    public GameObject gameSaverObject;
    public GameSaver gameSaver;

	UIBank uiBank;
    
	public GameObject[,] grid;
    //public List<int> environmentStructList;

    public int ppu = 32;

	public int cityNumber;

    public List<Environment> basicEnvironmentsList;

    public enum EnvironmentEnum
    {
        SaltFlats
    };

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
        basicEnvironmentsList = new List<Environment>();
        gameSaver = gameSaverObject.GetComponent<GameSaver>();
    }

    // Use this for initialization
    void Start () {
        CreateWorld();
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
		uiBank.OpenInfoPanel ();
        gameReady = true;
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

        if (gameSaver.ProfileExists())
        {
            gameSaver.LoadGame();
            playerCity.GetComponent<City>().FillArmySelectCB();
        }
        else
        {
            //spawn player city
            int px = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
            int py = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
            playerCity = Instantiate(city, new Vector3(px, py, 99), Quaternion.identity) as GameObject;
            playerCity.GetComponent<City>().position.x = (int)freeCoordinates[0].x;
            playerCity.GetComponent<City>().position.y = (int)freeCoordinates[0].y;
            grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = playerCity;
            freeCoordinates.RemoveAt(0);
            playerCity.GetComponent<City>().type = "your home";
            allCities.Add(playerCity.GetComponent<City>());

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

            while (freeCoordinates.Count > 0)
            {
                SpawnEnvironment((int)freeCoordinates[0].x, (int)freeCoordinates[0].y, saltFlats);
                freeCoordinates.RemoveAt(0);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	}

    private void SpawnEnvironment(int x, int y, GameObject g)
    {
        int px = (int)grid[x, y].transform.position.x;
        int py = (int)grid[x, y].transform.position.y;
        GameObject spawnedEnvironment = Instantiate(g, new Vector3(px, py, 99), Quaternion.identity) as GameObject;
        spawnedEnvironment.GetComponent<Environment>().position.x = x;
        spawnedEnvironment.GetComponent<Environment>().position.y = y;
        grid[x,y].GetComponent<Tile>().environment = spawnedEnvironment;
        basicEnvironmentsList.Add(spawnedEnvironment.GetComponent<Environment>());
    }

    private void SpawnCity()
    {
        int sx = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.x;
        int sy = (int)grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].transform.position.y;
        GameObject spawnedCity = Instantiate(city, new Vector3(sx, sy, 99), Quaternion.identity) as GameObject;
        grid[(int)freeCoordinates[0].x, (int)freeCoordinates[0].y].GetComponent<Tile>().environment = spawnedCity;
        
        cityList.Add(spawnedCity.GetComponent<City>());
        allCities.Add(spawnedCity.GetComponent<City>());
        spawnedCity.GetComponent<City>().position.x = (int)freeCoordinates[0].x;
        spawnedCity.GetComponent<City>().position.y = (int)freeCoordinates[0].y;
        //create armies
        for (int i = 0; i < 2; ++i)
        {
            spawnedCity.GetComponent<City>().CreateArmy();
        }

        freeCoordinates.RemoveAt(0);
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

        playerCity.GetComponent<City>().RemoveIfDestroyed();
        for (int i = 0; i < cityList.Count; ++i)
        {
            cityList[i].RemoveIfDestroyed();
        }

        if (cityList.Count < 1)
        {
            Debug.Log("You Win!");
        }
        uiBank.selectedTile.SimulateMouseClick();
        gameSaver.SaveGame();
    }
}
