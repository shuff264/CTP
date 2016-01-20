using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX;
	public int tileY;

	int randomX;
	int randomY;

	public List<Node> currentPath = null;

	Vector3 startPosition;
	public Vector3 endPosition;
	float speed = 1.0f;
	
	float startTime;
	float journeyLength;


	public TileMap tm;
	public VehicleSpawn vs;
	
	// Use this for initialization
	void Start () {
	
		vs = GameObject.Find ("Controller").GetComponent<VehicleSpawn> ();
		tm = GameObject.Find("Map").GetComponent<TileMap>();

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);
		
	//	int randX = Random.Range (0, roadFinder.roadPieces.Length - 1);
		//		float randY = Random.Range (0, 100);
		
		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.z;

		GenerateEndPosition();

		endPosition = new Vector3 (randomX, 1, randomY);


		currentPath = tm.GeneratePathTo(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

		if(currentPath == null){
//			vs.currentNumberOfVehicles--;
			Destroy(gameObject);
			

		}
		
	}

	void Update(){
		if(currentPath != null){
	
			int currentNode = 0;

			while(currentNode < currentPath.Count-1){
				Vector3 start = new Vector3 (currentPath[currentNode].x, 1, currentPath[currentNode].y);
				Vector3 end   = new Vector3 (currentPath[currentNode].x, 1, currentPath[currentNode].y);



				currentNode++;
			}
		}

		MoveNextTile();

	}

	public void MoveNextTile(){
		if(currentPath == null){
		//	vs.currentNumberOfVehicles--;
			Destroy(gameObject);
		}

		currentPath.RemoveAt(0);

//		float distCovered = (Time.time - startTime) * speed;
//		journeyLength = Vector3.Distance (startPosition, endPosition);
//		float fracJourney = distCovered / journeyLength;
//	
//		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, fracJourney);

		transform.position = new Vector3(currentPath[0].x, 1, currentPath[0].y);


		if(currentPath.Count <= 1){
			//vs.currentNumberOfVehicles--;
			currentPath = null;
			Destroy(gameObject);
		}
	}

	void GenerateEndPosition(){

		while(!tm.MovementAllowed(randomX, randomY)){
			randomX = Random.Range(0,24);
			randomY = Random.Range(0,24);
		}
	}
}
