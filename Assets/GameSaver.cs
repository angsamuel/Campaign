using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

//DOES ALL THINGS SERIALIZATION
public class GameSaver : MonoBehaviour {
    //environments
    //cities
    //
    private string savePath = "/Saves/Test/";

    void Awake()
    {
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveGame()
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        if (!Directory.Exists(Application.dataPath + savePath))
            {
                //if it doesn't, create it
                Directory.CreateDirectory(Application.dataPath + savePath);
            }
        SaveEnvironments(gameController.basicEnvironmentsList);
    }

    public void LoadGame()
    {
        LoadEnvironments();
    }

    //ENVIRONMENT SAVING---------------------------------------------------------------------------------
    [Serializable]
    public class SavableEnvironment
    {
        public string name;
        public string type;
        public int population;
        public int posX;
        public int posY;
        public int posZ;
    };

    [Serializable]
    public class SavableEnvironments
    {
        public SavableEnvironment[] savableEnvironments;
    };
    public void SaveEnvironments(List<Environment> le) 
    {
        SavableEnvironments se = new SavableEnvironments();
        se.savableEnvironments = new SavableEnvironment[le.Count];
        for(int i = 0; i<le.Count; ++i)
        {
            se.savableEnvironments[i] = new SavableEnvironment();
            se.savableEnvironments[i].name = le[i].name;
            se.savableEnvironments[i].type = le[i].type;
            se.savableEnvironments[i].population = le[i].population;
            se.savableEnvironments[i].posX = (int)le[i].position.x;
            se.savableEnvironments[i].posY = (int)le[i].position.y;
            se.savableEnvironments[i].posZ = le[i].posZ;
        }
        string environmentsToJson = JsonUtility.ToJson(se);
        System.IO.File.WriteAllText(Application.dataPath + savePath + "environments.json", environmentsToJson);
    }

    public void LoadEnvironments()
    {
        string fileString = System.IO.File.ReadAllText(Application.dataPath + savePath + "environments.json");
        SavableEnvironments se = new SavableEnvironments();
        se = JsonUtility.FromJson<SavableEnvironments>(fileString);
        for(int i = 0; i<se.savableEnvironments.Length; ++i)
        {
            LoadEnvironment(se.savableEnvironments[i]);
        }
    }

    public void LoadEnvironment(SavableEnvironment se)
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        switch (se.type)
        {
            default: //basic location
                Vector3 spawnLocation = new Vector3(gameController.grid[se.posX, se.posY].transform.position.x, gameController.grid[se.posX, se.posY].transform.position.y, se.posZ);
                GameObject tempSaltFlats = Instantiate(gameController.saltFlats, spawnLocation, Quaternion.identity) as GameObject;
                Environment e = tempSaltFlats.GetComponent<Environment>();
                e.name = se.name;
                e.type = se.type;
                e.population = se.population;
                e.position.x = se.posX;
                e.position.y = se.posY;
                gameController.basicEnvironmentsList.Add(e);
                gameController.grid[se.posX, se.posY].GetComponent<Tile>().environment = tempSaltFlats;
                break;
        }  
    }
}
