using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
    /* Create World
     * Manage Turns
     * Check For Win Condition
     * */
    public int height;
    public int width;
    GameObject tile;
    private GameObject[,] grid;
    public int ppu = 32;


    // Use this for initialization
    void Start () {
        CreateWorld();
        
	}

    private void CreateWorld()
    {
        Debug.Log(((Screen.height / 32f) - height) / 2f);
        grid = new GameObject[width, height];
        tile = Resources.Load("Prefabs/Tile") as GameObject;
        for (int ix = 0; ix < width; ix++)
        {
            for (int iy = 0; iy < height; iy++)
            {
				float xLoc = ix - (width/2);
				float yLoc = iy - (height/2);
				GameObject spawnedTile = Instantiate(tile, new Vector3(xLoc, yLoc, 0), Quaternion.identity) as GameObject;

                grid[ix, iy] = spawnedTile;
            }
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
