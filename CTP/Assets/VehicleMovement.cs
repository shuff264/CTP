using UnityEngine;
using System.Collections;

public class VehicleMovement : MonoBehaviour {

	Vector3 startPosition;
	public Vector3 endPosition;
	float speed = 1.0f;

	float startTime;
	float journeyLength;

	// Use this for initialization
	void Start () {

		float randX = Random.Range (0, 100);
		float randY = Random.Range (0, 100);

		startTime = Time.time;
		startPosition = gameObject.transform.position;
		endPosition = new Vector3 (randX, 0, randY);
		journeyLength = Vector3.Distance (startPosition, endPosition);
	
	}
	
	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, fracJourney);
	
	}
}
