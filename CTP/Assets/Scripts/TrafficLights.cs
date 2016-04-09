using UnityEngine;
using System.Collections;

public class TrafficLights : MonoBehaviour {

	public bool xGreen;
	public bool zGreen;

	float timeOnGreen;
	float timePassedOnGreen = 0f;

	// Use this for initialization
	void Awake () {
		timeOnGreen = Random.Range(2f, 6f);
		xGreen = returnBoolean();
		zGreen = !xGreen;

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

	bool returnBoolean(){

		return (Random.value > 0.5f);
	}
}
