using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public bool mouseOver = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Mouse enter");
		mouseOver = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse exit");
		mouseOver = false;
	}


}
