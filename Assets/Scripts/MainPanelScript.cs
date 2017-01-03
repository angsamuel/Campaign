using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public bool mouseOver = false;

	UIBank uiBank;
	// Use this for initialization
	void Start () {
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("Mouse enter");
		uiBank.mouseOnUI = true;
		mouseOver = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("Mouse exit");
		uiBank.mouseOnUI = false;
		mouseOver = false;
	}

	public void ToggleActive () {
		gameObject.SetActive (!gameObject.activeSelf);
	}

	public void MakeActive(){
		gameObject.SetActive (true);
	}
		
}
