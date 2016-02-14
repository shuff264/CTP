using UnityEngine;
using UnityEngine.UI;
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
	public float speed = 0f;
	
	float startTime;
	float journeyLength;

	public TileMap tm;
	public VehicleSpawn vs;

	float maxSpeed = 0;
	float currentSpeed;
	float acceleration = 0.2f;

	Rigidbody rb;
	// Use this for initialization
	void Start () {

		tm = GameObject.Find("Map").GetComponent<TileMap>();
		vs = GameObject.Find("Controller").GetComponent<VehicleSpawn>();
		rb = gameObject.GetComponent<Rigidbody> ();

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.z;

		GenerateEndPosition();

		endPosition = new Vector3 (randomX, 0.75f, randomY);

		currentPath = tm.FindPathToGoal(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

		if(currentPath == null){
			Destroy(gameObject);
		}



	}

	void FixedUpdate(){
		maxSpeed = tm.tileTypes [tm.tiles[currentPath [0].x, currentPath [0].y]].maxSpeed;

		if (speed <= maxSpeed) {
			speed += acceleration;
		}
		MoveNextTile();
	}

	public void MoveNextTile(){
		if(currentPath == null){
			Destroy(gameObject);
		}

		//startPosition = new Vector3 (currentPath[0].x, 1, currentPath[0].y); 
		//endPosition = new Vector3 (currentPath[1].x, 1, currentPath[1].y); 

		//THIS SHOULD DO THE BETTER MOVEMENT
		//rb.AddForce (transform.forward * speed);


		journeyLength = Vector3.Distance (new Vector3(currentPath[0].x, 0.75f, currentPath[0].y), new Vector3 (currentPath[1].x, 0.75f, currentPath[1].y));

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (new Vector3(currentPath[0].x, 0.75f, currentPath[0].y), new Vector3 (currentPath[1].x, 0.75f, currentPath[1].y), fracJourney);

		//ROTATION STUFF

		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentPath[0].x, 0.75f, currentPath[0].y) - transform.position);
		float str = Mathf.Min ((speed * 10) * Time.deltaTime, 1);

		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);

		if(gameObject.transform.position == new Vector3(currentPath[1].x, 0.75f, currentPath[1].y)){
			startTime = Time.time;
			currentPath.RemoveAt(0);
		}



		if(currentPath.Count <= 1){
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
