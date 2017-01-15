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
        if (gameController.gameReady)
        {
            string constructedString = "";
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

            }

            constructedString += "Proceed to coordinates (" + uiBank.selectedTile.posX + ", " + uiBank.selectedTile.posY + "). ";
            switch (primaryObjectiveString)
            {
                case "deploy to":
                    break;
                case "ravage":
                    constructedString += "Leave no one left alive.";
                    break;
                default:
                    break;
            }

            if (secondaryObjectiveString == "hold position")
            {
                constructedString += "Hold position upon completion of primary objective.";
            }
            else if (secondaryObjectiveString == "return home")
            {
                constructedString += " Return home after completion of primary objective.";
            }
            OrderText.text = constructedString;
        }
    }

    public void GrabArmyFromPlayerCity()
    {
        if (gameController.playerCity.GetComponent<City>().armyTable.ContainsKey(uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text)) {
            Army armyInQuestion = gameController.playerCity.GetComponent<City>().armyTable[uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text] as Army;
            uiBank.soldiersNumText.text = armyInQuestion.soldiers.ToString();
        }
   }
    //issues order to army based on UI
    public void IssueOrder()
    {
        string armyString = uiBank.ArmySelectCB.GetComponent<Kender.uGUI.ComboBox>()._comboTextRectTransform.GetComponent<Text>().text;

        switch (armyString)
        {
            case "...":
                break;
            default:
                if (gameController.playerCity.GetComponent<City>().armyTable.Contains(armyString))
                {
                    Debug.Log("army found in table at least!");
                }
                Army a = gameController.playerCity.GetComponent<City>().armyTable[armyString] as Army;
                AssignOrders(a);
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
                armyToBeOrdered.OrderDeployTo(uiBank.selectedTile.posX, uiBank.selectedTile.posY, true);
                break;
            case "ravage":
                armyToBeOrdered.OrderRavage(uiBank.selectedTile.posX, uiBank.selectedTile.posY, true);
                break;
            case "capture":
                armyToBeOrdered.OrderCapture(uiBank.selectedTile.posX, uiBank.selectedTile.posY, true);
                break;
        }
        switch (secondaryObjectiveString)
        {
            case "return home":
                armyToBeOrdered.OrderReturnHome(false);
                break;
            default:
                break;
        }
        
    }
}