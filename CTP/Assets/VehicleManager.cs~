using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition; //Where the vehicle starts
	Vector3 currentPosition; //The intermidiary positon it is currently moving to
	public Vector3 endPosition; //The end goal
	float speed = 1.0f;
		
	public RoadFinder roadFinder;

	public TileMap map;

	public GameObject currentTile;
	Vector2[] vecArray;
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, roadFinder.roadPieces.Length);
		int randY = Random.Range (0, roadFinder.roadPieces.Length);
	
		startPosition = gameObject.transform.position;
		endPosition = roadFinder.roadPieces[randX].transform.position;
		endPosition.y++;

		currentTile = map.TileReturn((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		currentPosition = currentTile.transform.position;
		currentPosition.y++;

	}



	// Update is called once per frame
	void Update () {


		if(gameObject.transform.position == currentPosition){

			//Gets the correct tiledata for the tile
			TileData td = map.tilesGrid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.z].GetComponent<TileData>();


			vecArray = new Vector2[4];

			if(td.tileNorthType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z+1);
				currentPosition = currentTile.transform.position;

				vecArray[0] = new Vector2(currentTile.transform.position.x,currentTile.transform.position.z);

			}

			if(td.tileEastType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x+1, (int)currentTile.transform.position.z);
				currentPosition = currentTile.transform.position;
				
				vecArray[1] = new Vector2(currentTile.transform.position.x,currentTile.transform.position.z);
				
			}

			if(td.tileSouthType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z-1);
				currentPosition = currentTile.transform.position;
				
				vecArray[2] = new Vector2(currentTile.transform.position.x,currentTile.transform.position.z);
				
			}

			if(td.tileEastType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x-1, (int)currentTile.transform.position.z);
				currentPosition = currentTile.transform.position;
				
				vecArray[3] = new Vector2(currentTile.transform.position.x,currentTile.transform.position.z);
				
			}

//			for(int i = 0; i < vecArray.Length; i++){
//
//				Debug.Log(vecArray[i]);
//
//			}
//			
			Vector2 searchValue = new Vector2(endPosition.x, endPosition.z); //Value to be found in list
			Vector2 currentNearest = vecArray[0]; //The current nearest value reflected as a position in list

			float currentDifferenceX = Mathf.Abs(currentNearest.x - searchValue.x);
			float currentDifferenceY = Mathf.Abs(currentNearest.y - searchValue.y);
			Vector2 currentDifference = new Vector2(currentDifferenceX, currentDifferenceY);
			//int currentDifference = Mathf.Abs(currentNearest - searchValue); //Works out the current different between the closest and the value
			
			for (int i = 0; i < vecArray.Length; i++)
			{
				//int diff = Mathf.Abs(vecArray[i] - searchValue); //value from array - search amount to get the difference

				float diffX = Mathf.Abs(vecArray[i].x - searchValue.x);
				float diffY = Mathf.Abs(vecArray[i].y - searchValue.y);

				Vector2 diff = new Vector2(diffX, diffY);

				if (diff.x < currentDifference.x){ //if the difference is less than current diffrence - if its closer than current closests
					if(diff.y < currentDifference.y){
						currentDifference = diff; //change current difference to this difference
						currentNearest = vecArray[i]; //change current nearest to this value
					}
				}
			}

//			Debug.Log(currentNearest);

			currentPosition = new Vector3(currentNearest.x, 1, currentNearest.y);


			gameObject.transform.position = currentPosition;
		}

	


		if (gameObject.transform.position == endPosition) {

			Debug.Log("DESTROY");
			DestroyObject(gameObject);
			
		}
	

	}
}

