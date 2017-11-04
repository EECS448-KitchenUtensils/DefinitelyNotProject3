﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameModel;

public class MainSceneStart : MonoBehaviour {

	public ChessGame game = new ChessGame();

	public Transform square_dark;
	public Transform square_light;
	public GameObject piece_bishop;
	public GameObject piece_knight;
	public GameObject piece_rook;
	public GameObject piece_king;
	public GameObject piece_queen;
	public GameObject piece_pawn;

	public float gridWidth;
	public float gridDepth;
	public Transform[, ] squares = new Transform[14, 14];
	public Dictionary<ChessPiece, GameObject>[] clientPiecesCollection = new Dictionary<ChessPiece, GameObject>[4];

	public bool moveState = false;


	// Use this for initialization
	void Start () {
		CreateGrid ();
		CreatePieces ();
		GivePiecesColors ();
		GivePiecesBehavior ();
	}

	// Update is called once per frame
	/// <summary>
	/// Main Update
	/// </summary>
	void Update () {
		//BishopTest ();
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

	/// <summary>
	/// Creates the pieces. Adds them to a dictionary for future use.
	/// </summary>
	void CreatePieces(){
		for(int i = 0; i < 4; i++){
			clientPiecesCollection[i] = new Dictionary<string, GameObject> ();
		}
		foreach(Dictionary<string, GameObject> dict in clientPiecesCollection){

			dict.Add (ChessPiece, (Instantiate (piece_king, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("queen", (Instantiate (piece_queen, new Vector3(1, 1, -1), Quaternion.identity)) );

			//2 Knights
			dict.Add ("knight0", (Instantiate (piece_knight, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("knight1", (Instantiate (piece_knight, new Vector3(1, 1, -1), Quaternion.identity)) );

			//2 Rooks
			dict.Add ("rook0", (Instantiate (piece_rook, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("rook1", (Instantiate (piece_rook, new Vector3(1, 1, -1), Quaternion.identity)) );


			//2 Bishops
			dict.Add ("bishop0", (Instantiate (piece_bishop, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("bishop1", (Instantiate (piece_bishop, new Vector3(1, 1, -1), Quaternion.identity)) );


			//8 Pawns.
			dict.Add ("pawn0", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn1", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn2", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn3", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn4", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn5", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn6", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );
			dict.Add ("pawn7", (Instantiate (piece_pawn, new Vector3(1, 1, -1), Quaternion.identity)) );

		}
	}

	/// <summary>
	/// Gives the pieces colors.
	/// </summary>
	void GivePiecesColors(){
		//Player 1, blue
		foreach (GameObject o in clientPiecesCollection[0].Values) {
			Renderer rend = o.GetComponent<Renderer> ();
			rend.material.color = new Color (0, 0, 1);
		}
		//Player 2, red
		foreach (GameObject o in clientPiecesCollection[1].Values) {
			Renderer rend = o.GetComponent<Renderer> ();
			rend.material.color = new Color (1, 0, 0);
		}
		//Player 3, yellow
		foreach (GameObject o in clientPiecesCollection[2].Values) {
			Renderer rend = o.GetComponent<Renderer> ();
			rend.material.color = new Color (1.0f, 0.92f, 0.016f);
		}
		//Player 4, green
		foreach (GameObject o in clientPiecesCollection[3].Values) {
			Renderer rend = o.GetComponent<Renderer> ();
			rend.material.color = new Color (0, 1, 0);
		}
	}

	void GivePiecesBehavior(){

		for (int i = 0; i < 4; i++) {
			foreach (string key in clientPiecesCollection[i].Keys) {
				clientPiecesCollection [i] [key].AddComponent<PieceBehavior> ();
				clientPiecesCollection [i] [key].GetComponent<PieceBehavior> ().thisPiece = key;
			}
		}

	}

//	void BishopTest(){
//		Renderer rend = test_bishop.GetComponent<Renderer>();
//		rend.material.color = new Color (2, 0, 0);
//		Vector3 current = test_bishop.transform.position;
//		Vector3 dest = new Vector3 (5, 5, -1);
//		Vector3 to = Vector3.MoveTowards (current, dest, 2 * Time.deltaTime);
//		test_bishop.transform.position = to;
//	}
}
