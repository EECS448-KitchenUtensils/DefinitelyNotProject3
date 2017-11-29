using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameModel;
using GameModel.Data;

public class PieceBehavior : MonoBehaviour {

    public BoardPosition currentPosition;
    public BoardPosition oldPosition;
	public float startx;
	public float starty;
	private GameObject[] tempSquare = new GameObject[50];
	private Vector3 mousePosition;
	private IArbitrator arby{
		get{
			return GameObject.Find ("GameObject_Main").GetComponent<MainSceneStart> ().arby;
		}
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(oldPosition != currentPosition)
        {
            transform.position = new Vector3((float)currentPosition.X, (float)currentPosition.Y - 1, -1);
            oldPosition = currentPosition;
        }

			
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
        oldPosition = new BoardPosition((XCoord)0, 1);
		float x = Mathf.Round(this.gameObject.transform.position.x);
		float y = Mathf.Round(this.gameObject.transform.position.y);
		foreach (var square in tempSquare) {
			if (square != null && square.transform.position.x == x && square.transform.position.y == y) {
                arby.MakeMove(currentPosition, new BoardPosition((XCoord)x, (int) y + 1));
			} 
		}
	}

	void OnMouseDown (){

        Debug.Log("heyyyyyyy");

		GameObject square = GameObject.Find ("whitesquare");

		var moves = arby.PossibleMoves(currentPosition);

		int count = 0;
		foreach (var move in moves) {
			tempSquare [count] = Instantiate (square, new Vector3 ((float)move.Destination.X, (float)move.Destination.Y - 1, -0.1f), Quaternion.identity);
			tempSquare [count].GetComponent<Renderer> ().material.color = this.gameObject.GetComponent<Renderer> ().material.color;
			count++;
		}
	}
}
