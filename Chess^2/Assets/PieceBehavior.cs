using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour {

	public string thisPiece;
	private GameObject tempSquare;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Destroy (tempSquare);
		}
	}

	void OnMouseDown (){

		GameObject square = GameObject.Find("sqaure_dark");

		if (thisPiece.Contains ("pawn")) {
			float x = this.gameObject.transform.position.x;
			float y = this.gameObject.transform.position.y;
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
