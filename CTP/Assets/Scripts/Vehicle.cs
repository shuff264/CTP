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
	float speed;
	
	float startTime;
	float journeyLength;

	public TileMap tm;
	public VehicleSpawn vs;

	
	// Use this for initialization
	void Start () {

		tm = GameObject.Find("Map").GetComponent<TileMap>();
		vs = GameObject.Find("Controller").GetComponent<VehicleSpawn>();

		speed = vs.speedSlider.value;

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.z;

		GenerateEndPosition();

		endPosition = new Vector3 (randomX, 0.75f, randomY);

		currentPath = tm.GeneratePathTo(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

		if(currentPath == null){
			Destroy(gameObject);
		}

	}

	void Update(){
		speed = vs.speedSlider.value;
		MoveNextTile();
	}

	public void MoveNextTile(){
		if(currentPath == null){
			Destroy(gameObject);
		}

		//startPosition = new Vector3 (currentPath[0].x, 1, currentPath[0].y); 
		//endPosition = new Vector3 (currentPath[1].x, 1, currentPath[1].y); 





		journeyLength = Vector3.Distance (new Vector3(currentPath[0].x, 0.75f, currentPath[0].y), new Vector3 (currentPath[1].x, 0.75f, currentPath[1].y));

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (new Vector3(currentPath[0].x, 0.75f, currentPath[0].y), new Vector3 (currentPath[1].x, 0.75f, currentPath[1].y), fracJourney);

		//ROTATION STUFF

		Quaternion targetRotation = Quaternion.LookRotation( transform.position - new Vector3(currentPath[0].x, 0.75f, currentPath[0].y));
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
