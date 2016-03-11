﻿using UnityEngine;
using System.Collections;

public class TrafficLights : MonoBehaviour {

	public bool xGreen = true;
	public bool zGreen = false;

	float timeOnGreen = 10f;
	float timePassedOnGreen = 0f;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		if(timePassedOnGreen >= timeOnGreen){
			xGreen = !xGreen;
			zGreen = !zGreen;
			timePassedOnGreen = 0f;
		}

		timePassedOnGreen += Time.deltaTime;
	}
}
