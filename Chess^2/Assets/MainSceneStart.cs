using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneStart : MonoBehaviour {

	public Transform square_dark;
	public Transform square_light;
	public float gridWidth;
	public float gridDepth;
	public Transform[, ] squares = new Transform[14, 14];
	public Dictionary<string, Transform>[] clientPiecesCollection = new Dictionary<string, Transform>[4];


	// Use this for initialization
	void Start () {
		CreateGrid ();
		CreatePieces ();
	}

	// Update is called once per frame
	void Update () {

	}


	//Pre: 
	void CreateGrid (){
		//Draws board and maps quares to array
		for (int y = 0; y < gridDepth; y=y+1) {
			for (int x = 0; x < gridWidth; x = x +1) {
				//This fat if statement creates the cut-outs. also squares[0][0] == null
				if (!(x < 3 && y < 3) && !(x >= 14-3 && y < 3) && !(x < 3 && y >= 14-3) && !(x >= 14-3 && y >= 14-3)) {
					if ((y % 2 == 0) && (x % 2 == 0)) {
						squares [x, y] = Instantiate (square_dark, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 0) && (x % 2 == 1)) {
						squares [x, y] = Instantiate (square_light, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 1) && (x % 2 == 0)) {
						squares [x, y] = Instantiate (square_light, new Vector3 (x, y, 0), Quaternion.identity);
					}
					if ((y % 2 == 1) && (x % 2 == 1)) {
						squares [x, y] = Instantiate (square_dark, new Vector3 (x, y, 0), Quaternion.identity);
					}
				}
			}
		}
	}

	void CreatePieces(){
		foreach(Dictionary<string, Transform> dict in clientPiecesCollection){

		}
	}
}
