using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition; //Where the vehicle starts
	Vector3 currentPosition; //The intermidiary positon it is currently moving to
	public Vector3 endPosition; //The end goal
	float speed = 1.0f;
		
	public RoadFinder roadFinder;

	public TileMap map;

	GameObject currentTile;
	
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, map.mapSizeX - 1);
		int randY = Random.Range (0, map.mapSizeY - 1);
	
		startPosition = gameObject.transform.position;
		endPosition = map.TileReturn(randX, randY).transform.position;
		endPosition.y++;

		
	}
	
	// Update is called once per frame
	void Update () {

		//Check four options on tile
		//choose best
		//set it as currentPosition
		//Repeat till at goal

		if (gameObject.transform.position == endPosition) {
			
			DestroyObject(gameObject);
			
		}
		
	}
}

