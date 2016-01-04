using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadFinder : MonoBehaviour {

	public GameObject[] roadPieces;

	// Use this for initialization
	void Start () {
	
		roadPieces = GameObject.FindGameObjectsWithTag ("road");
		
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
