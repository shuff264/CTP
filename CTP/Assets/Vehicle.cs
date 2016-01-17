using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX;
	public int tileY;

	public List<TileMap.Node> currentPath = null;

	Vector3 startPosition;
	public Vector3 endPosition;
	float speed = 1.0f;
	
	float startTime;
	float journeyLength;
	
	public RoadFinder roadFinder;
	public TileMap tm;
	
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		tm = GameObject.Find("Map").GetComponent<TileMap>();
		
		int randX = Random.Range (0, roadFinder.roadPieces.Length - 1);
		//		float randY = Random.Range (0, 100);
		
		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.y;
		endPosition = new Vector3 (roadFinder.roadPieces [randX].transform.position.x, 1, roadFinder.roadPieces [randX].transform.position.z);
		journeyLength = Vector3.Distance (startPosition, endPosition);

		currentPath = tm.GeneratePathTo(tileX, tileY, (int)endPosition.x, (int)endPosition.z);
		
	}

}
