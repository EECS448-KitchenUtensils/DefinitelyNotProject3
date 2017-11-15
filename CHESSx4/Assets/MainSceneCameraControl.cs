using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Move main camera to center of grid.
		float gridWidth = GameObject.Find ("GameObject_Main").GetComponent<MainSceneStart> ().gridWidth;
		float gridDepth = GameObject.Find ("GameObject_Main").GetComponent<MainSceneStart> ().gridDepth;
		float cam_x = (gridWidth - 1) / 2;
		float cam_y = (gridDepth - 1) / 2;
		Vector3 cam_coords = new Vector3(cam_x, cam_y, -10);
		Camera.main.transform.position = cam_coords;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
