using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameModel;
using GameModel.Data;

public class PieceBehavior : MonoBehaviour {

	public ChessPiece thisPiece;
	public float startx;
	public float starty;
	private GameObject[] tempSquare = new GameObject[50];
	private Vector3 mousePosition;
	private ChessGame Game{
		get{
			return GameObject.Find ("GameObject_Main").GetComponent<MainSceneStart> ().game;
		}
	}


	// Use this for initialization
	void Start () {
		Vector3 position = new Vector3 ((float)thisPiece.Position.X, (float)thisPiece.Position.Y - 1, -1);
		transform.position = position;
	}
	
	// Update is called once per frame
	void Update () {
			
		if(Input.GetMouseButtonUp(0)){
			foreach (var o in tempSquare) {
				Destroy (o);
			}
		}
	}


	//Theses need to be changed to work with ChessPiece as key instead of tree.


	void OnMouseDrag() {
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = Vector3.Lerp (transform.position, new Vector3(mousePosition.x, mousePosition.y, -1), 0.5f);
	}

	void OnMouseUp() {
		float x = Mathf.Round(this.gameObject.transform.position.x);
		float y = Mathf.Round(this.gameObject.transform.position.y);
		foreach (var square in tempSquare) {
			if (square != null && square.transform.position.x == x && square.transform.position.y == y) {
				var lastmovetype = Game.MakeMove(thisPiece.Position, new BoardPosition((XCoord) x, (int) y+1));
				if(lastmovetype == MoveType.Capture){
					DestroyCapturedPieces();
				}
			} 
		}
		Vector3 position = new Vector3 ((float)thisPiece.Position.X, (float)thisPiece.Position.Y - 1, -1);
		transform.position = position;
	}

	void OnMouseDown (){

		GameObject square = GameObject.Find ("whitesquare");

		var moves = Game.PossibleMoves (thisPiece.Position);

		int count = 0;
		foreach (var move in moves) {
			tempSquare [count] = Instantiate (square, new Vector3 ((float)move.Position.X, (float)move.Position.Y - 1, -0.1f), Quaternion.identity);
			tempSquare [count].GetComponent<Renderer> ().material.color = this.gameObject.GetComponent<Renderer> ().material.color;
			count++;
		}
	}

	void DestroyCapturedPieces(){
		var oldPieces = GameObject.Find ("GameObject_Main").GetComponent<MainSceneStart> ().clientPiecesCollection;
		var newPieces = Game.Pieces;
		foreach (var pieceDict in oldPieces) {
			var deadPieces = pieceDict.Keys.Except (newPieces);
			foreach (var deadPiece in deadPieces) {
				Destroy (pieceDict [deadPiece]);
			}
		}
	}
}
