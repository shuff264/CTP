using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX;
	public int tileY;

	public List<Node> currentPath = null;

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
		tileY = (int)startPosition.z;
		endPosition = new Vector3 (roadFinder.roadPieces [randX].transform.position.x, 1, roadFinder.roadPieces [randX].transform.position.z);


		currentPath = tm.GeneratePathTo(tileX, tileY, (int)endPosition.x, (int)endPosition.z);
		
	}

	void Update(){
		if(currentPath != null){
	
			int currentNode = 0;

			while(currentNode < currentPath.Count-1){
				Vector3 start = new Vector3 (currentPath[currentNode].x, 1, currentPath[currentNode].y);
				Vector3 end   = new Vector3 (currentPath[currentNode].x, 1, currentPath[currentNode].y);

				Debug.DrawLine(start, end, Color.red);

				currentNode++;
			}
		}

		MoveNextTile();

	}

	public void MoveNextTile(){
		if(currentPath == null){
			return;
		}


//		float distCovered = (Time.time - startTime) * speed;
//		journeyLength = Vector3.Distance (startPosition, endPosition);
//		float fracJourney = distCovered / journeyLength;
//	
//		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, fracJourney);

		transform.position = new Vector3(currentPath[0].x, 1, currentPath[0].y);


		if(currentPath.Count == 1){
			currentPath = null;
			Destroy(gameObject);
		}
	}

}
