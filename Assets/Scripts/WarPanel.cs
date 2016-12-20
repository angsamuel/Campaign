using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarPanel : MonoBehaviour {
    //manages view on all input areas
    UIBank uiBank;
    GameController gameController;

    string previousString;
    string newString;

    void Awake()
    {
        uiBank = GameObject.Find("UIBank").GetComponent<UIBank>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    void LateUpdate()
    {
        //update information about correct army
        newString = uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;
        if (newString != previousString)
        {
            previousString = newString;
            GrabArmyFromPlayerCity();
        }
    }

    public void GrabArmyFromPlayerCity()
    {
        //if (ContainsKey(gameController.playerCity.GetComponent<City>().armyTable.ContainsKey(uiBank.ArmySelectCB)){ }

        if (gameController.playerCity.GetComponent<City>().armyTable.ContainsKey(uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text)) {
            GameObject armyInQuestion = gameController.playerCity.GetComponent<City>().armyTable[uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text] as GameObject;
            uiBank.soldiersNumText.text = armyInQuestion.GetComponent<Army>().soldiers.ToString();
        }

       // {//
 //
 //   
        }
}