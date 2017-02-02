using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
public class MainMenuController : MonoBehaviour {
    List<String> profiles;
	public Dropdown profileSelectDD;
    public string[] files;
    public GameObject LoadGamePanel;
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLoadGamePanel()
    {
        LoadGamePanel.SetActive(true);
    }

    void Awake()
    {
        LoadGamePanel.SetActive(false);
        profiles = new List<String>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("profile", "This will literally never be a profile");

        files = Directory.GetDirectories(Application.dataPath + "/Saves/");
        for(int i = 0; i < files.Length; ++i)
        {
            files[i] = Path.GetFileName(files[i]);
        }
        Array.Sort<String>(files);
    }

	// Use this for initialization
	void Start () {
        //profileSelectCB.GetComponent<Kender.uGUI.ComboBox>().ClearItems();
        //profileSelectCB.GetComponent<Kender.uGUI.ComboBox>().AddItems(files);
		profileSelectDD.ClearOptions ();
		profileSelectDD.AddOptions (files.ToList());
    }
	
    void LateUpdate()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}
    
    public void StartNewGame()
    {
        PlayerPrefs.SetString("profile", "This will literally never be a profile");
        Application.LoadLevel("OverWorld");
    }

    public void LoadSavedGame()
    {
        //PlayerPrefs.SetString("profile", profileSelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text);
		PlayerPrefs.SetString ("profile", profileSelectDD.GetComponentInChildren<Text> ().text);
		Application.LoadLevel("OverWorld");
    }
}
