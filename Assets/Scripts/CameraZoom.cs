using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
    public Camera cam;
    public float cameraCurrentZoom;
    public float cameraZoomMax;
    private float ppuScale = 1f;

	// Use this for initialization
	void Start () {
        cameraCurrentZoom = ((Screen.height) / (ppuScale * 32.0f)) * 0.5000f;
        Debug.Log(Screen.height);
        cameraZoomMax = cameraCurrentZoom;
        cam.GetComponent<Camera>().orthographicSize = cameraCurrentZoom;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
