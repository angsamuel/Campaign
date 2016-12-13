using UnityEngine;
using System.Collections;

public class NameWizard : MonoBehaviour {

	string[] villageNounArr;
	string[] namesArr;
	string[] adjectiveArr;
	string[] nounArr;
	string[] maleNamesArr;
	string[] femaleNamesArr;
	string[] lastNamesArr;


	// Use this for initialization
	void Start () {
		loadWordLists();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public string GenerateCityName(){
		string returnString = "";
		int format = Random.Range (0, 16);

		//add prefix
		int prefix = Random.Range (0, 2);
		if (prefix < 1) {
			returnString += "";
		} else if (prefix < 2) {
			returnString += "The ";
		}


		if (format < 1) {
			returnString = RandomName () + "'s " + RandomVillageNoun ();
		} else if (format < 2) {
			returnString += RandomAdjective () + " " + RandomNoun () + " " + RandomVillageNoun ();
		} else if (format < 3) {
			returnString += RandomNoun () + " of " + RandomName ();
		} else if (format < 4) {
			returnString += RandomAdjective () + " " + RandomVillageNoun ();
		} else if (format < 5) {
			returnString += RandomNoun () + " " + RandomAdjective ();
		} else if (format < 6) {
			returnString += RandomVillageNoun () + " of " + RandomNoun ();
		} else if (format < 7) {
			string noun1 = RandomNoun();
			string noun2 = RandomNoun ();
			if (noun1.Substring (noun1.Length - 1, 1) == "y") {
				noun1 = noun1.Substring(0, noun1.Length-1) + "ies";
			} else if (noun1.Substring (noun1.Length - 1, 1) != "s") {
				noun1 = noun1 + "s";
			}
			if (noun2.Substring (noun2.Length - 1, 1) == "y") {
				noun2 = noun2.Substring(0, noun2.Length-1) + "ies";
			} else if (noun2.Substring (noun2.Length - 1, 1) != "s") {
				noun2 = noun2 + "s";
			}

			returnString += RandomVillageNoun() + " of " + noun1 + " and " + noun2;
		} else if (format < 8) {
			returnString += RandomAdjective () + " " + RandomAdjective ();
		} else if (format < 9) {
			returnString += RandomNoun ();
		} else if (format < 10) {
			returnString = RandomAdjective () + " " + RandomVillageNoun () + " of " + RandomName ();
		} else if (format < 11) {
			returnString = RandomNoun ();
			int postFix = Random.Range (0, 4);
			if (postFix < 1) {
				returnString += "ville";
			} else if (postFix < 2) {
				returnString += "ton";
			} else if (postFix < 3) {
				returnString = "Santa " + returnString;
			} else if (postFix < 4) {
				returnString += "ham";
			}
		} else if (format < 12) {
			returnString = RandomName ();
		} else if (format < 13) {
			returnString += RandomAdjective ();
		} else if (format < 14) {
			returnString += RandomVillageNoun () + " of " + RandomName (); 
		} else if (format < 15) {
			returnString += RandomAdjective () + " " + RandomVillageNoun ();
		} else if (format < 16) {
			returnString = RandomAdjective () + " " + RandomVillageNoun () + " of " + RandomNoun ();
		}else {
			returnString += RandomAdjective () + " " + RandomNoun () + " " + RandomVillageNoun ();
		}

		return returnString;

	}

	private void destroyWordLists(){

		namesArr = new string[0];
		nounArr = new string[0];
		adjectiveArr = new string[0];
		villageNounArr = new string[0];
		maleNamesArr = new string[0];
		femaleNamesArr = new string[0];
		lastNamesArr = new string[0];

		Resources.UnloadUnusedAssets ();
	}

	private string RandomNoun(){
		return nounArr [Random.Range (0, nounArr.Length)];
	}
	private string RandomName(){
		return namesArr [Random.Range (0, namesArr.Length)];
	}
	private string RandomAdjective(){
		return adjectiveArr [Random.Range (0, adjectiveArr.Length)];
	}
	private string RandomVillageNoun(){
		return villageNounArr [Random.Range (0, villageNounArr.Length)];
	}
	private string RandomMaleName(){
		return maleNamesArr [Random.Range (0, maleNamesArr.Length)];
	}
	private string RandomFemaleName(){
		return femaleNamesArr [Random.Range (0, femaleNamesArr.Length)];
	}
	private string RandomLastName(){
		return lastNamesArr [Random.Range (0, lastNamesArr.Length)];
	}

	private void loadWordLists(){
		TextAsset villageNounsAsset = Resources.Load ("Words/village_nouns") as TextAsset;
		TextAsset nounsAsset = Resources.Load ("Words/nouns") as TextAsset;
		TextAsset adjectivesAsset = Resources.Load ("Words/adj") as TextAsset;
		TextAsset namesAsset = Resources.Load ("Words/all_names") as TextAsset;
		TextAsset maleNamesAsset = Resources.Load ("Words/male_names") as TextAsset;
		TextAsset femaleNamesAsset = Resources.Load ("Words/female_names") as TextAsset;
		TextAsset lastNamesAsset = Resources.Load ("Words/last_names") as TextAsset;

		char[] archDelim = new char[] { '\r', '\n' };

		villageNounArr = villageNounsAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		nounArr = nounsAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		adjectiveArr = adjectivesAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		namesArr = namesAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		maleNamesArr = maleNamesAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		femaleNamesArr = femaleNamesAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
		lastNamesArr = lastNamesAsset.text.Split(archDelim, System.StringSplitOptions.RemoveEmptyEntries); 
	}

	//public string GetFemaleName

}
