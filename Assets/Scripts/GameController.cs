﻿using UnityEngine;
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

	GameObject tile;
	GameObject city;

	UIBank uiBank;
    
	private GameObject[,] grid;
    public int ppu = 32;

	public int cityNumber;

	//Player Data
	public GameObject playerCity;

	List<Vector3> freeCoordinates = new List<Vector3>();

    // Use this for initialization
    void Start () {
        CreateWorld();
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
		uiBank.OpenInfoPanel ();
		//GameObject x = GameObject.Find ("ArmySelectCB") as GameObject;
		//x.GetComponent<Kender.uGUI.ComboBox> ().AddItems(new List<string>(){"John","Jill","Jack"});
	}

    private void CreateWorld()
    {
		//spawn tiles-----------------------------------------------------------
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
		grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].GetComponent<Tile> ().environment = playerCity;
		freeCoordinates.RemoveAt(0);
		playerCity.GetComponent<City> ().FillArmySelectCB ();

		//spawn Cities
		for (int i = 1; i < cityNumber; ++i) {
			int sx = (int)grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].transform.position.x; 
			int sy = (int)grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].transform.position.y;
			GameObject spawnedCity = Instantiate (city,new Vector3(sx, sy, 99), Quaternion.identity) as GameObject;
			grid [(int)freeCoordinates [0].x, (int)freeCoordinates [0].y].GetComponent<Tile> ().environment = spawnedCity;
			freeCoordinates.RemoveAt(0);
		}
    }

	// Update is called once per frame
	void Update () {
	}
	public int GetMapRows(){
		return height;
	}
	public int GetMapCols(){
		return width;
	}
}
