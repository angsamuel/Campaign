﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
	
	private Vector2 minSize = new Vector2 (160, 32);
	private Vector2 maxSize = new Vector2 (Screen.height/2, Screen.height/2.5f);
	
	private RectTransform panelRectTransform;
	private Vector2 originalLocalPointerPosition;
	private Vector2 originalSizeDelta;
	
	void Awake () {
		panelRectTransform = transform.parent.GetComponent<RectTransform> ();
	}
	
	public void OnPointerDown (PointerEventData data) {
		originalSizeDelta = panelRectTransform.sizeDelta;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
	}
	
	public void OnDrag (PointerEventData data) {
		if (panelRectTransform == null)
			return;
		
		Vector2 localPointerPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
		Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
		
		Vector2 sizeDelta = originalSizeDelta + new Vector2 (offsetToOriginal.x, -offsetToOriginal.y);
		sizeDelta = new Vector2 (
			Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x),
			Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y)
		);
		
		panelRectTransform.sizeDelta = sizeDelta;
	}
}