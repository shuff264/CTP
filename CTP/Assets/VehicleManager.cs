using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition;
	public Vector3 endPosition;
	float speed = 1.0f;
	
	float startTime;
	float journeyLength;
	
	public RoadFinder roadFinder;

	public TileMap map;
	
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, map.mapSizeX - 1);
		int randY = Random.Range (0, map.mapSizeY - 1);
		
		startTime = Time.time;
		startPosition = gameObject.transform.position;
		endPosition = map.TileReturn(randX, randY).transform.position;
		endPosition.y++;
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

