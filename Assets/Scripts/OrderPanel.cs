using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour {
    //manages view on all input areas
    public UIBank uiBank;
    GameController gameController;

    public Text OrderHeader;
    public Text OrderText;

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
        string armyString = uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;
        bool orderIsFalid = false;
        if (armyString == "...")
        {
            OrderHeader.text = "PLEASE SPECIFY COMMANDER";
        }
        else
        {
            OrderHeader.text = "My Faithful " + armyString;
        }
        string primaryObjectiveString = uiBank.actionSelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;
        string secondaryObjectiveString = uiBank.returnActionSelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;

        if (primaryObjectiveString == "deploy to")
        {
            OrderText.text = "Proceed to coordinates (" + uiBank.selectedTile.posX +", " + uiBank.selectedTile.posY + ").";
        }
      
    }

    public void GrabArmyFromPlayerCity()
    {
        if (gameController.playerCity.GetComponent<City>().armyTable.ContainsKey(uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text)) {
            GameObject armyInQuestion = gameController.playerCity.GetComponent<City>().armyTable[uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text] as GameObject;
            uiBank.soldiersNumText.text = armyInQuestion.GetComponent<Army>().soldiers.ToString();
        }
   }
    //issues order to army based on UI
    public void IssueOrder()
    {
        string armyString = uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;

        Army armyToBeOrdered;
        switch (armyString)
        {
            case "...":
                break;
            default:
                GameObject a = gameController.playerCity.GetComponent<City>().armyTable[armyString] as GameObject;
                armyToBeOrdered = a.GetComponent<Army>();
                AssignOrders(armyToBeOrdered);
                break;
        }

    }
    private void AssignOrders(Army armyToBeOrdered)
    {
        string primaryObjectiveString = uiBank.actionSelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;
        string secondaryObjectiveString = uiBank.returnActionSelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;
        switch (primaryObjectiveString)
        {
            case "deploy to":
                armyToBeOrdered.OrderDeployTo(uiBank.selectedTile.posX, uiBank.selectedTile.posY);
                break;
            default:
                break;
        }
    }
}