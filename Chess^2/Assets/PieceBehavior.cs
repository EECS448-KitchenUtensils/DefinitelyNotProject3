using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour {

	public string thisPiece;
	public float startx;
	public float starty;
	private GameObject tempSquare;
	private Vector3 mousePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Destroy (tempSquare);
		}
	}

	void OnMouseDrag() {
		if (thisPiece.Contains ("pawn")) {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
			transform.position = Vector2.Lerp (transform.position, mousePosition, 0.5f);
		}
	}
	void OnMouseUp() {
		if (thisPiece.Contains ("pawn")) {
			float x = Mathf.Round(this.gameObject.transform.position.x);
			float y = Mathf.Round(this.gameObject.transform.position.y);
			if (tempSquare.transform.position.x == x && tempSquare.transform.position.y == y) {
				this.gameObject.transform.position = new Vector3 (tempSquare.transform.position.x, tempSquare.transform.position.y, -1);
			} 
			else {
				this.gameObject.transform.position = new Vector3 (startx, starty, -1);
			}
		}
	}

	void OnMouseDown (){

		GameObject square = GameObject.Find("sqaure_dark");

		if (thisPiece.Contains ("pawn")) {
			float x = this.gameObject.transform.position.x;
			float y = this.gameObject.transform.position.y;
			startx = x;
			starty = y;
			y += 2;
			tempSquare = Instantiate (square, new Vector3 (x, y, -0.1f), Quaternion.identity);
			tempSquare.GetComponent<Renderer> ().material.color = new Color (0, 0, 1);


		} else if (thisPiece.Contains ("king")) {

		} else if (thisPiece.Contains ("queen")) {

		} else if (thisPiece.Contains ("rook")) {

		} else if (thisPiece.Contains ("knight")) {

		} else if (thisPiece.Contains ("bishop")) {

		} else {

		}
	}
}
