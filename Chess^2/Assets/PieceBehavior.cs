using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour {

	public string thisPiece;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown (){
		if (thisPiece.Contains ("pawn")) {

		} else if (thisPiece.Contains ("king")) {

		} else if (thisPiece.Contains ("queen")) {

		} else if (thisPiece.Contains ("rook")) {

		} else if (thisPiece.Contains ("knight")) {

		} else if (thisPiece.Contains ("bishop")) {

		} else {

		}
	}
}
