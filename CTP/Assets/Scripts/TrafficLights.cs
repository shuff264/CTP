﻿using UnityEngine;
using System.Collections;

public class TrafficLights : MonoBehaviour {

	//Two bools to control the two directions of travel
	public bool xGreen;
	public bool zGreen;

	//Total time the lights have been on a certain direction
	float timeOnGreen;
	float timePassedOnGreen = 0f;

	//Randomly generates the time it takes to change
	//Randomly selects which direction is on green (UP/DOWN OR LEFT/RIGHT)
	void Awake () {
		timeOnGreen = Random.Range(2f, 6f);
		xGreen = returnBoolean();
		zGreen = !xGreen;

	}
	
	//When enough time is elapsed the directions switch
	void Update () {
		if(timePassedOnGreen >= timeOnGreen){
			xGreen = !xGreen;
			zGreen = !zGreen;
			timePassedOnGreen = 0f;
		}

		timePassedOnGreen += Time.deltaTime;
	}

	//Returns a random boolean
	bool returnBoolean(){

		return (Random.value > 0.5f);
	}
}
