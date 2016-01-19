using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadFinder : MonoBehaviour {

	public GameObject[] roadPieces;

	// Use this for initialization
	void Update () {
		roadPieces = GameObject.FindGameObjectsWithTag ("road");
	}

}
