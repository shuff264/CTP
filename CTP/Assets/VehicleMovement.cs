using UnityEngine;
using System.Collections;

public class VehicleMovement : MonoBehaviour {

	Vector3 startPosition;
	public Vector3 endPosition;
	float speed = 1.0f;

	float startTime;
	float journeyLength;

	public RoadFinder roadFinder;

	// Use this for initialization
	void Start () {

		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		

		int randX = Random.Range (0, roadFinder.roadPieces.Length - 1);
//		float randY = Random.Range (0, 100);

		startTime = Time.time;
		startPosition = gameObject.transform.position;
		endPosition = new Vector3 (roadFinder.roadPieces [randX].transform.position.x, 1, roadFinder.roadPieces [randX].transform.position.z);
		journeyLength = Vector3.Distance (startPosition, endPosition);
	
	}
	
	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, fracJourney);


		if (gameObject.transform.position == endPosition) {

			DestroyObject(gameObject);

		}
	
	}
}
