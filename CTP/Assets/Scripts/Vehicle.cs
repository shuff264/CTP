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

	
	// Use this for initialization
	void Start () {

		tm = GameObject.Find("Map").GetComponent<TileMap>();

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.z;

		GenerateEndPosition();

		endPosition = new Vector3 (randomX, 1, randomY);

		currentPath = tm.GeneratePathTo(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

		if(currentPath == null){
			Destroy(gameObject);
		}
		
	}

	void Update(){
		MoveNextTile();
	}

	public void MoveNextTile(){
		if(currentPath == null){
			Destroy(gameObject);
		}

		currentPath.RemoveAt(0);

		transform.position = new Vector3(currentPath[0].x, 1, currentPath[0].y);
	

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
