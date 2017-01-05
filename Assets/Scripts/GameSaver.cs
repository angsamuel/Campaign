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
    public GameObject army;

    private string savePath;

    void Awake()
    {
        savePath = Application.dataPath + "/Saves/Test/";
        RefreshSavePath();
        army = Resources.Load("Prefabs/Army") as GameObject;
    }

    public void RefreshSavePath()
    {
        savePath = Application.dataPath + "/Saves/" + PlayerPrefs.GetString("profile") + "/";
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool ProfileExists()
    {
        return (Directory.Exists(savePath));
    }

    public void SaveGame()
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();
        RefreshSavePath();
        if (!Directory.Exists(savePath))
            {
                //if it doesn't, create it
                Directory.CreateDirectory(savePath);
            }
        SaveEnvironments(gameController.basicEnvironmentsList);
        SaveCities(gameController.allCities);
    }

    public void LoadGame()
    {
        LoadEnvironments();
        LoadCities();
    }

    //CHARACTER SAVING-----------------------------------------------------------------------------------
    [Serializable]
    public class SavableCharacter
    {
        public string firstName;
        public string lastName;
        public string profession;
    }

    [Serializable]
    public class SavableCharacters
    {
        public SavableCharacter[] savableCharacters;
    }

    public SavableCharacter SaveCharacter(Character c)
    {
        SavableCharacter sc = new SavableCharacter();
        sc.firstName = c.firstName;
        sc.lastName = c.lastName;
        sc.profession = c.profession;

        return sc;
    }

    public SavableCharacters SaveCharacters(List<Character> lc)
    {
        SavableCharacters sc = new SavableCharacters();
        sc.savableCharacters = new SavableCharacter[lc.Count];
        for(int i = 0; i<lc.Count; ++i)
        {
            sc.savableCharacters[i] = SaveCharacter(lc[i]);
        }
        return sc;
    }

    public Character LoadCharacter(SavableCharacter sc)
    {
        Character c = new Character();
        c.firstName = sc.firstName;
        c.lastName = sc.lastName;
        c.profession = sc.profession;
        return c;
    }
    //ARMY SAVING----------------------------------------------------------------------------------------
    [Serializable]
    public class SavableArmy
    {
        public SavableCharacter leader;
        public int soldiers;
        public int posX;
        public int posY;
        public bool isStored;

        public Army.Objective primaryObjective = Army.Objective.none;
        public int primaryObjectivePosX;
        public int primaryObjectivePosY;

        public Army.Objective secondaryObjective = Army.Objective.none;
        public int secondaryObjectivePosX;
        public int secondaryObjectivePosY;
        //something to designate sprite later on*/
    }

    [Serializable]
    public class SavableArmies
    {
        public SavableArmy[] savableArmies;
    }

    public SavableArmies SaveArmies(List<Army> la)
    {
        SavableArmies sa = new SavableArmies();
        sa.savableArmies = new SavableArmy[la.Count];
        for(int i = 0; i<la.Count; ++i)
        {
            sa.savableArmies[i] = new SavableArmy();
            sa.savableArmies[i].leader = SaveCharacter(la[i].leader);
            sa.savableArmies[i].soldiers = la[i].soldiers;
            sa.savableArmies[i].posX = (int)la[i].position.x;
            sa.savableArmies[i].posY = (int)la[i].position.y;
            sa.savableArmies[i].isStored = la[i].isStored;

            //objectives
            sa.savableArmies[i].primaryObjective = la[i].primaryObjective;
            sa.savableArmies[i].primaryObjectivePosX = (int)la[i].primaryObjectiveLoc.x;
            sa.savableArmies[i].primaryObjectivePosY = (int)la[i].primaryObjectiveLoc.y;

            sa.savableArmies[i].secondaryObjective = la[i].secondaryObjective;
            sa.savableArmies[i].secondaryObjectivePosX = (int)la[i].secondaryObjectiveLoc.x;
            sa.savableArmies[i].secondaryObjectivePosY = (int)la[i].secondaryObjectiveLoc.y;
        }
        return sa;
    }
    
    public List<Army> LoadArmies(SavableArmies sa)
    {
        GameObject gameControllerObject = GameObject.Find("GameController") as GameObject;
        GameController gameController = gameControllerObject.GetComponent<GameController>();

        List<Army> la = new List<Army>();
        for(int i = 0; i<sa.savableArmies.Length; ++i)
        {
            GameObject armyObject = Instantiate(army, new Vector3(1000, 1000, -1000), Quaternion.identity) as GameObject;
            Army a = armyObject.GetComponent<Army>();

            a.leader = LoadCharacter(sa.savableArmies[i].leader);
            a.soldiers = sa.savableArmies[i].soldiers;
            a.position.x = sa.savableArmies[i].posX;
            a.position.y = sa.savableArmies[i].posY;
            a.isStored = sa.savableArmies[i].isStored;
            
            //objectives
            a.primaryObjective = sa.savableArmies[i].primaryObjective;
            a.primaryObjectiveLoc.x = sa.savableArmies[i].primaryObjectivePosX;
            a.primaryObjectiveLoc.y = sa.savableArmies[i].primaryObjectivePosY;

            a.secondaryObjective = sa.savableArmies[i].primaryObjective;
            a.secondaryObjectiveLoc.x = sa.savableArmies[i].secondaryObjectivePosX;
            a.secondaryObjectiveLoc.y = sa.savableArmies[i].secondaryObjectivePosY;

            armyObject.transform.position = new Vector3(1000, 1000, -1000);
            la.Add(a);
        }
        return la;
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
        public Color color;
        public SavableCharacter leader;
        public SavableArmies armies;
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
            sc.savableCities[i].color = lc[i].GetComponent<Renderer>().material.color;
            sc.savableCities[i].leader = SaveCharacter(lc[i].leader);
            sc.savableCities[i].armies = SaveArmies(lc[i].armies);
        }
        string environmentsToJson = JsonUtility.ToJson(sc);
        System.IO.File.WriteAllText(savePath + "cities.json", environmentsToJson);
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
        tempCity.GetComponent<City>().GetComponent<Renderer>().material.color = sc.color;
        tempCity.GetComponent<City>().leader = LoadCharacter(sc.leader);

        //tempCity.GetComponent<City>().armies.Clear();
        //tempCity.GetComponent<City>().armies = LoadArmies(sc.armies);

        if (isPlayerCity)
        {
            tempCity.GetComponent<City>().ResetArmyTable();
        }
        Debug.Log("armytable size" + tempCity.GetComponent<City>().armyTable.Count);
        List<Army> loadedArmies = LoadArmies(sc.armies);
        for (int i = 0; i <loadedArmies.Count; ++i)
        {
            Army a = tempCity.GetComponent<City>().CreateArmy();
            a.leader = loadedArmies[i].leader;
            a.soldiers = loadedArmies[i].soldiers;
            a.position.x = loadedArmies[i].position.x;
            a.position.y = loadedArmies[i].position.y;
            a.isStored = loadedArmies[i].isStored;

            //objectives -- currently no objctive is stored in loadedarmiesF
            a.primaryObjective = loadedArmies[i].primaryObjective;
            a.primaryObjectiveLoc.x = loadedArmies[i].primaryObjectiveLoc.x;
            a.primaryObjectiveLoc.y = loadedArmies[i].primaryObjectiveLoc.y;

            a.secondaryObjective = loadedArmies[i].secondaryObjective;
            a.secondaryObjectiveLoc.x = loadedArmies[i].secondaryObjectiveLoc.x;
            a.secondaryObjectiveLoc.y = loadedArmies[i].secondaryObjectiveLoc.y;

            if (!a.isStored)
            {
                GameObject tempTile = gameController.grid[(int)a.position.x, (int)a.position.y];
                a.transform.position = new Vector3(tempTile.transform.position.x, tempTile.transform.position.y, a.posZ);
                a.rulerCity.storedArmies.Remove(a);
            }
            tempCity.GetComponent<City>().ResetArmyTable();
        }

        gameController.grid[sc.posX, sc.posY].GetComponent<Tile>().environment = tempCity;
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
        string fileString = System.IO.File.ReadAllText(savePath + "cities.json");
        SavableCities sc = new SavableCities();
        sc = JsonUtility.FromJson<SavableCities>(fileString);
        LoadCity(sc.savableCities[0], true);
        for (int i = 1; i < sc.savableCities.Length; ++i)
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
        System.IO.File.WriteAllText(savePath + "environments.json", environmentsToJson);
    }

    public void LoadEnvironments()
    {
        string fileString = System.IO.File.ReadAllText(savePath + "environments.json");
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
