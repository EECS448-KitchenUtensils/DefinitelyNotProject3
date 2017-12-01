using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBehavior : MonoBehaviour {

    /// <summary>
    /// Holds the value of the current turn
    /// </summary>
    public int turn = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
    /// Change color of the turn indicator if new turn.
    /// </summary>
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
