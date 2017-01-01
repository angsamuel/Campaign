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

    public bool ProfileExists()
    {
        return (Directory.Exists(Application.dataPath + savePath));
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
        SaveCities(gameController.allCities);
    }

    public void LoadGame()
    {
        LoadEnvironments();
        LoadCities();
    }

    //CITY SAVING----------------------------------------------------------------------------------------
    [Serializable]
    public class SavableCity
    {
        public string name;
        public string type;
        public int population;
        public int posX;
        public int posY;
        public int posZ;
        //color needs to go here
    }

    [Serializable]
    public class SavableCities
    {
        public SavableCity[] savableCities;
    };

    public void SaveCities(List<City> lc)
    {
        SavableCities sc = new SavableCities();
        sc.savableCities = new SavableCity[lc.Count];
        for(int i = 0; i<lc.Count; ++i)
        {
            sc.savableCities[i] = new SavableCity();
            sc.savableCities[i].name = lc[i].name;
            sc.savableCities[i].type = lc[i].type;
            sc.savableCities[i].population = lc[i].population;
            sc.savableCities[i].posX = (int)lc[i].position.x;
            sc.savableCities[i].posY = (int)lc[i].position.y;
            sc.savableCities[i].posZ = (int)lc[i].posZ;
        }
        string environmentsToJson = JsonUtility.ToJson(sc);
        System.IO.File.WriteAllText(Application.dataPath + savePath + "cities.json", environmentsToJson);
    }

    private void LoadCity(SavableCity sc, bool isPlayerCity)
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        
        Vector3 spawnLocation = new Vector3(gameController.grid[sc.posX, sc.posY].transform.position.x, gameController.grid[sc.posX, sc.posY].transform.position.y, sc.posZ);
        GameObject tempCity = Instantiate(gameController.city, spawnLocation, Quaternion.identity) as GameObject;
        tempCity.GetComponent<City>().name = sc.name;
        tempCity.GetComponent<City>().type = sc.type;
        tempCity.GetComponent<City>().population = sc.population;
        tempCity.GetComponent<City>().position.x = sc.posX;
        tempCity.GetComponent<City>().position.y = sc.posY;

        gameController.grid[sc.posX, sc.posY].GetComponent<Tile>().environment = tempCity;
        gameController.playerCity = tempCity;
        gameController.allCities.Add(tempCity.GetComponent<City>());

        if (isPlayerCity)
        {
            gameController.playerCity = tempCity;
        }
        else
        {
            gameController.cityList.Add(tempCity.GetComponent<City>());
        }
    }
    public void LoadCities()
    {
        string fileString = System.IO.File.ReadAllText(Application.dataPath + savePath + "cities.json");
        SavableCities sc = new SavableCities();
        sc = JsonUtility.FromJson<SavableCities>(fileString);
        for (int i = 0; i < sc.savableCities.Length; ++i)
        {
            LoadCity(sc.savableCities[i], false);
        }
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

    private void LoadEnvironment(SavableEnvironment se)
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        switch (se.type)
        {
            default: //basic location
                Vector3 spawnLocation = new Vector3(gameController.grid[se.posX, se.posY].transform.position.x, gameController.grid[se.posX, se.posY].transform.position.y, se.posZ);
                GameObject tempSaltFlats = Instantiate(gameController.saltFlats, spawnLocation, Quaternion.identity) as GameObject;
                tempSaltFlats.GetComponent<Environment>().name = se.name;
                tempSaltFlats.GetComponent<Environment>().type = se.type;
                tempSaltFlats.GetComponent<Environment>().population = se.population;
                tempSaltFlats.GetComponent<Environment>().position.x = se.posX;
                tempSaltFlats.GetComponent<Environment>().position.y = se.posY;
                gameController.basicEnvironmentsList.Add(tempSaltFlats.GetComponent<Environment>());
                gameController.grid[se.posX, se.posY].GetComponent<Tile>().environment = tempSaltFlats;
                break;
        }  
    }
}
