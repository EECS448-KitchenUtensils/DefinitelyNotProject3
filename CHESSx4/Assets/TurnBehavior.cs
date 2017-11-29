using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBehavior : MonoBehaviour {

    public int turn = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (turn) {
		case 0:
			GetComponent<Renderer> ().material.color = new Color (0, 0, 1);
			break;
		case 1:
			GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
			break;
		case 2:
			GetComponent<Renderer> ().material.color = new Color (1.0f, 0.92f, 0.016f);
			break;
		case 3:
			GetComponent<Renderer> ().material.color = new Color (00, 1, 0);
			break;
		}
	}
}
